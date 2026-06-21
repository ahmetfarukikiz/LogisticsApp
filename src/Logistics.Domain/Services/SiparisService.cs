using Logistics.Domain.Entities;
using Logistics.Domain.Entities.Enums;
using Logistics.Domain.Entities.Kullanicilar;
using Logistics.Domain.Entities.Siparisler;
using Logistics.Domain.Entities.Siparisler.States;
using Logistics.Domain.Entities.Urunler;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Resolvers;
using Logistics.Domain.Services.KargoManager;
using Logistics.Domain.Services.OdemeManager;

namespace Logistics.Domain.Services;

// Sipariş yaşam döngüsünü yöneten servis 
public class SiparisService
{
    private readonly ISiparisRepository _siparisRepository;
    private readonly StokService _stokService;
    private readonly IKargoResolver _kargoResolver;
    private readonly IOdemeResolver _odemeResolver;

    // İşlemdeki geçici sipariş verileri 
    private Siparis _taslakSiparis;
    private decimal _sonTutar;
    private Musteri _islemYapanMusteri;

    public SiparisService(
        ISiparisRepository siparisRepository,
        StokService stokService,
        IKargoResolver kargoResolver,
        IOdemeResolver odemeResolver)
    {
        _siparisRepository = siparisRepository;
        _stokService = stokService;
        _kargoResolver = kargoResolver;
        _odemeResolver = odemeResolver;
    }

    public Siparis AktifSiparisGetir()
    {
        return _siparisRepository.SiparisGetir();
    }

    public List<(int Id, string Isim, decimal Fiyat, int Stok, double Agirlik)> UrunKatalogunuGetir()
    {
        return _stokService.UrunleriGetir()
            .Cast<UrunBase>()
            .Select(u => (u.Id, u.Isim, u.FiyatHesapla(), u.Stok, u.AgirlikHesapla()))
            .ToList();
    }

    // Sequence Diagram: StokKontrolEt(UrunId): stokVarMi (bool)
    public bool StokKontrolEt(int urunId)
    {
        var urun = _stokService.UrunGetir(urunId);
        return urun != null && urun.StokVarMi;
    }

    // Sequence Diagram: SiparisOlustur(UrunId, kargoTipi, List<HizmetTipi>)
    // KargoService.KargoIslemleriniYap(KargoDetayDTO): (KargoUcret, TakipNo)
    public SiparisOzetiDTO SiparisOlustur(int urunId, int kargoTipi, string hizmetGirdisi, Musteri musteri)
    {
        _islemYapanMusteri = musteri;
        var urun = _stokService.UrunGetir(urunId);

        if (urun == null || !urun.StokVarMi)
            return new SiparisOzetiDTO { Hata = "Geçersiz ürün veya stok yetersiz!" };

        if (!_kargoResolver.GecerliMi(kargoTipi))
            return new SiparisOzetiDTO { Hata = "Geçersiz kargo seçimi!" };

        var kargoDetay = new KargoDetayDTO
        {
            Agirlik = urun.AgirlikHesapla(),
            Mesafe = 500, // Simüle edilmiş mesafe
            Hizmetler = HizmetleriParsele(hizmetGirdisi)
        };

        // Sequence Diagram: KargoIslemleriniYap(KargoDetayDTO): (KargoUcret, TakipNo)
        var kargoAdaptor = _kargoResolver.Coz(kargoTipi);
        var kargoService = new KargoService(kargoAdaptor);
        var (kargoUcreti, takipNo) = kargoService.KargoIslemleriniYap(kargoDetay);

        _sonTutar = urun.FiyatHesapla() + kargoUcreti;

        // Sequence Diagram: <<create>> siparis
        _taslakSiparis = new Siparis(new Beklemede())
        {
            Id = 1,
            Urun = urun,
            OdenenTutar = _sonTutar,
            KargoTakipNo = takipNo,
            KargoUcreti = kargoUcreti
        };

        return new SiparisOzetiDTO
        {
            KargoUcreti = kargoUcreti,
            TakipNo = takipNo,
            ToplamTutar = _sonTutar,
            KargoIsmi = _kargoResolver.IsimGetir(kargoTipi)
        };
    }

    // Sequence Diagram: OdemeYap(OdemeTuru) OdemeService.OdemeYap(OdemeTuru, SonTutar)
    public (bool Basarili, string Mesaj) OdemeYap(int odemeTuru)
    {
        if (_taslakSiparis == null || _islemYapanMusteri == null)
            return (false, "Bekleyen bir sipariş işlemi bulunamadı!");

        if (!_odemeResolver.GecerliMi(odemeTuru))
            return (false, "Geçersiz ödeme yöntemi!");

        // Sequence Diagram: OdemeYap(OdemeTuru, SonTutar)
        var odemeStrategy = _odemeResolver.Coz(odemeTuru);
        var odemeService = new OdemeService(odemeStrategy);
        var (basarili, mesaj) = odemeService.OdemeYap(_islemYapanMusteri, _sonTutar);

        if (!basarili) return (false, mesaj);

        string odemeYontemiAdi = _odemeResolver.IsimGetir(odemeTuru);
        // Sequence Diagram: IslemiLogla(Ödeme Onaylandı)
        LogManager.GetLogger().IslemiLogla($"SPS Ödeme Onaylandı: {odemeYontemiAdi} ile {_sonTutar:N2} TL alındı.");

        // Sequence Diagram: StokDusur() + IslemiLogla(Stok Güncellendi)
        _stokService.StokDusur(_taslakSiparis.Urun);
        LogManager.GetLogger().IslemiLogla($"Stok Güncellendi: {((UrunBase)_taslakSiparis.Urun).Isim} stoğu düşürüldü.");

        _siparisRepository.SiparisAyarla(_taslakSiparis);

        _taslakSiparis = null;
        _sonTutar = 0;

        return (true, "Sipariş Onaylandı");
    }

    public string DurumuIlerlet()
    {
        var siparis = _siparisRepository.SiparisGetir();
        if (siparis == null) return null;

        var eskiDurum = siparis.GuncelDurum();
        siparis.IleriDurumGecis();
        var yeniDurum = siparis.GuncelDurum();

        // Eğer kargodan iadeye düştüyse (Otomatik İade)
        if (eskiDurum == "Kargoda" && yeniDurum == "İade Edildi")
        {
            IadeVeStokIslemleriniUygula(siparis, _islemYapanMusteri);
        }

        LogManager.GetLogger().IslemiLogla($"Sipariş durumu değişti: {eskiDurum} -> {yeniDurum}");
        return yeniDurum;
    }

    public void SiparistenVazgec(Musteri musteri)
    {
        var siparis = _siparisRepository.SiparisGetir();
        if (siparis == null) return;

        bool iptalMi = siparis.IptalEdilebilirMi();
        var urun = siparis.Urun;
        var iade = siparis.OdenenTutar;

        siparis.SiparistenVazgec();

        IadeVeStokIslemleriniUygula(siparis, musteri);

        string islemTuru = iptalMi ? "İptal Edildi" : "İade Edildi";
        LogManager.GetLogger().IslemiLogla(
            $"Sipariş {islemTuru}: {((UrunBase)urun).Isim}, İade edilen tutar: {iade} TL");
    }

    private void IadeVeStokIslemleriniUygula(Siparis siparis, Musteri musteri)
    {
        _stokService.StokArtir(siparis.Urun);

        // Kapsülleme: bakiye artışı Musteri nesnesinin kendi metodu üzerinden
        if (musteri != null)
        {
            musteri.BakiyeEkle(siparis.OdenenTutar);
        }
    }

    public void SiparisiTemizle()
    {
        _siparisRepository.SiparisSil();
    }


    //seçilen girdiyi enumdaki karşılıklarına göre mapler ve gelen stringi bu mape göre çözer.
    private List<HizmetTuru> HizmetleriParsele(string girdi)
    {
        var hizmetler = new List<HizmetTuru>();
        if (string.IsNullOrWhiteSpace(girdi) || girdi.Trim() == "0")
            return hizmetler;

        var hizmetMap = new Dictionary<string, HizmetTuru>
        {
            { "1", HizmetTuru.Sigortali },
            { "2", HizmetTuru.KirilacakEsya }
        };

        foreach (var parca in girdi.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            if (hizmetMap.TryGetValue(parca.Trim(), out var hizmet) && !hizmetler.Contains(hizmet))
                hizmetler.Add(hizmet);
        }
        return hizmetler;
    }
}

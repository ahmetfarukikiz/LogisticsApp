using Logistics.Domain.Entities.Kullanicilar;
using Logistics.Domain.Entities.Siparisler;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Services;
using Logistics.Presentation.Views.AnaEkranlar;
using Logistics.Presentation.Views.Surecler;

namespace Logistics.Presentation.Controllers;

// Müşteri sipariş sürecini siparisService'e ileten controller
// sadece UI'ı yönetir ve ilgili servisi çağırır.
public class SiparisController
{
    private readonly SiparisService _siparisService; // sadece siparisService
                                                     // ile haberleşir siparisService kendi içinde diğer servisleri kullanır
    private readonly SiparisView _siparisView = new();
    private readonly Musteri _musteri;

    public SiparisController(SiparisService siparisService, IKullaniciRepository kullaniciRepository)
    {
        _siparisService = siparisService;
        _musteri = kullaniciRepository.TumKullanicilariGetir().OfType<Musteri>().First();
    }

    public void PaneliBaslat(MusteriView view)
    {
        while (true)
        {
            var siparis = _siparisService.AktifSiparisGetir();
            //güncel durumun ismi alınır
            var durum = siparis?.GuncelDurum();
            bool siparisAktif = durum != null && durum != "İptal Edildi" && durum != "İade Edildi";

            view.Bakiye = _musteri.Bakiye;
            view.SiparisDurumu = durum;
            view.UrunListesiAktif = !siparisAktif;
            view.VazgecAktif = siparisAktif;
            //iade edilebilirse iade et yazar. iptal edilebiliyorsa iptal et yazar.
            view.VazgecEtiketi = siparisAktif && siparis != null
                ? (siparis.IptalEdilebilirMi() ? "İptal Et" : "İade Et")
                : "İptal Et";
            view.SonrakiSiparisAktif = durum == "Teslim Edildi";

            view.EkraniCiz();
            string secim = view.GirdiAl();

            if (secim == "0") break;

            if (secim == "1" && !siparisAktif)
            {
                SiparisVer();
                if (_siparisService.AktifSiparisGetir() != null)
                {
                    ViewiGuncelle(view);
                    view.EkraniCiz();
                    System.Threading.Thread.Sleep(1500);
                    _siparisService.DurumuIlerlet();
                }
                continue;
            }

            if (secim == "2" && siparisAktif)
            {
                _siparisService.SiparistenVazgec(_musteri);
                ViewiGuncelle(view);
                view.EkraniCiz();
                System.Threading.Thread.Sleep(1500);
                _siparisService.SiparisiTemizle();
                continue;
            }

            if (secim == "3" && view.SonrakiSiparisAktif)
            {
                _siparisService.SiparisiTemizle();
                continue;
            }
        }
    }

    //siparis bilgilerinde bir güncelleme olduğunda çağrılır. ekrandaki bilgileri günceller
    private void ViewiGuncelle(MusteriView view)
    {
        var siparis = _siparisService.AktifSiparisGetir();
        var durum = siparis?.GuncelDurum();
        bool aktif = durum != null && durum != "İptal Edildi" && durum != "İade Edildi";

        view.Bakiye = _musteri.Bakiye;
        view.SiparisDurumu = durum;
        view.UrunListesiAktif = !aktif;
        view.VazgecAktif = aktif;
        view.VazgecEtiketi = aktif && siparis != null
            ? (siparis.IptalEdilebilirMi() ? "İptal Et" : "İade Et")
            : "İptal Et";
        view.SonrakiSiparisAktif = durum == "Teslim Edildi";
    }

    private void SiparisVer()
    {
        //Ürünleri listele ve seçim al
        var urunKatalogu = _siparisService.UrunKatalogunuGetir();
        _siparisView.UrunleriGoster(urunKatalogu);

        int urunId = _siparisView.UrunSecimiAl();
        if (urunId == 0) return;

        //stok kontrolü stokta yoksa sipariş verilemez
        bool stokVarMi = _siparisService.StokKontrolEt(urunId);
        if (!stokVarMi)
        {
            _siparisView.HataMesaji("Stok Yetersiz!");
            return;
        }

        // Kargo ve Hizmet seçimlerini al
        _siparisView.KargoSecenekleriniGoster();
        int kargoSecimiId = _siparisView.KargoSecimiAl();

        _siparisView.HizmetSecenekleriniGoster();
        string hizmetGirdisi = _siparisView.HizmetSecimiAl();

        // Servis bize kargo ücretini, toplam fiyatı hesaplayıp döner
        SiparisOzetiDTO spOzet = _siparisService.SiparisOlustur(urunId, kargoSecimiId, hizmetGirdisi, _musteri);

        if (!spOzet.Basarili)
        {
            _siparisView.HataMesaji(spOzet.Hata);
            return;
        }

        // Ürünün kargo fiyatı ve son halini ekrana bas.
        _siparisView.KargoDetayGoster(spOzet.KargoIsmi, spOzet.KargoUcreti, spOzet.TakipNo,
            spOzet.ToplamTutar - spOzet.KargoUcreti, spOzet.ToplamTutar);

        // Ödeme yöntemi seç
        _siparisView.OdemeSecenekleriniGoster();
        int odemeSecimId = _siparisView.OdemeSecimiAl();

        // Bakiye düşme, kargo stratejisini çalıştırma, Stok loglama işlemi için servis sınıfı çağrılır
        var odemeSonucu = _siparisService.OdemeYap(odemeSecimId);

        // Sonucu Göster
        if (odemeSonucu.Basarili)
            _siparisView.OdemeOnayMesaji();
        else
            _siparisView.HataMesaji(odemeSonucu.Mesaj);
    }
}

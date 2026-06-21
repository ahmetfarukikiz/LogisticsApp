using Logistics.Domain.Entities.Kullanicilar;
using Logistics.Domain.Entities.Urunler;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Services;
using Logistics.Presentation.Views.AnaEkranlar;
using Logistics.Presentation.Views.Surecler;

namespace Logistics.Presentation.Controllers;

// Depo yöneticisi işlemlerini yöneten controller
public class DepoController
{
    private readonly SiparisService _siparisService;
    private readonly StokService _stokService;
    private readonly IKullaniciRepository _kullaniciRepository;
    private readonly DepoIslemView _islemView = new();

    public DepoController(
        SiparisService siparisService,
        StokService stokService,
        IKullaniciRepository kullaniciRepository)
    {
        _siparisService = siparisService;
        _stokService = stokService;
        _kullaniciRepository = kullaniciRepository;
    }

    // Controller döngüyü yönetir view sadece ekrana basar ve girdi alır
    public void PaneliBaslat(DepoView view)
    {
        while (true)
        {
            // domainden veri çek
            var durum = _siparisService.AktifSiparisGetir()?.GuncelDurum();
            bool hazirlamaAktif = durum == "Onaylandı";
            bool kargoyaVerAktif = durum == "Hazırlanıyor";

            // view'a verileri taşı
            view.SiparisDurumu = durum;
            view.HazirlamaAktif = hazirlamaAktif;
            view.KargoyaVerAktif = kargoyaVerAktif;
            view.EkraniCiz();

            string secim = view.GirdiAl();

            //console uygulaması olduğu için seçimler mecburen if ile
            if (secim == "0") break;
            if (secim == "1" && hazirlamaAktif) { DurumIlerlet(); continue; }
            if (secim == "2" && kargoyaVerAktif) { DurumIlerlet(); continue; }
            if (secim == "3") { UrunListesiGoster(); continue; }
            if (secim == "4") { BildirimleriGoster(); continue; }
        }
    }

    private void DurumIlerlet()
    {
        var siparis = _siparisService.AktifSiparisGetir();
        if (siparis == null) { _islemView.SiparisYokMesaji(); return; }

        var eskiDurum = siparis.GuncelDurum();
        _siparisService.DurumuIlerlet();
        var yeniDurum = siparis.GuncelDurum();
        _islemView.DurumGuncelleMesaji(eskiDurum, yeniDurum);
    }

    private void BildirimleriGoster()
    {
        var depoYoneticisi = _kullaniciRepository.TumKullanicilariGetir()
            .OfType<DepoYoneticisi>().FirstOrDefault();
        _islemView.BildirimleriGoster(depoYoneticisi?.GetBildirimler());
    }

    private void UrunListesiGoster()
    {
        //base classtaki propları kullanabilmek için UrunBase'e çevirip listeye atıp tek seferde gösteriyor
        var urunler = _stokService.UrunleriGetir()
            .Cast<UrunBase>()
            .Select(u => (u.Isim, u.Stok, u.Sinir))
            .ToList();
        _islemView.UrunListesiGoster(urunler);
    }
}

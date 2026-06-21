using Logistics.Domain.Services;
using Logistics.Presentation.Views.AnaEkranlar;
using Logistics.Presentation.Views.Surecler;

namespace Logistics.Presentation.Controllers;

// Kurye işlemlerini yöneten controller
// View'ı bilir ve yönetir view Controller'ı tanımaz
public class KuryeController
{
    private readonly SiparisService _siparisService;
    private readonly KuryeIslemView _islemView = new();

    public KuryeController(SiparisService siparisService)
    {
        _siparisService = siparisService;
    }

    // Controller döngüyü yönetir view sadece ekrana basar ve girdi alır
    public void PaneliBaslat(KuryeView view)
    {
        while (true)
        {
            // Controller domain'den bilgileri alır
            var durum = _siparisService.AktifSiparisGetir()?.GuncelDurum();
            bool teslimEdilebilir = durum == "Kargoda";

            // Controller view'a bilgileri iletir
            view.SiparisDurumu = durum;
            view.TeslimAktifMi = teslimEdilebilir;

            view.EkraniCiz();
            string secim = view.GirdiAl();

            // routing yönlendirme 
            if (secim == "0") break;

            if (secim == "1" && teslimEdilebilir)
            {
                TeslimEt();
                continue;
            }
        }
    }

    //kurye teslim et seçeneğini seçerse bu metot çalışır 
    private void TeslimEt()
    {
        var siparis = _siparisService.AktifSiparisGetir();
        if (siparis == null) return;

        var eskiDurum = siparis.GuncelDurum();

        // Hatalı ürün kontrolü ve UI animasyonu 
        if (siparis.Urun.HataliMi)
        {
            _islemView.HataAnimasyonuGoster();
            LogManager.GetLogger().IslemiLogla($"Sipariş #{siparis.Id} hatalı ürün nedeniyle iade edildi");
        }

        _siparisService.DurumuIlerlet();
        var yeniDurum = siparis.GuncelDurum();
        if(!siparis.Urun.HataliMi)
            _islemView.TeslimOnayMesaji(eskiDurum, yeniDurum);
    }
}

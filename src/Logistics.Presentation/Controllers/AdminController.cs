using Logistics.Domain.Interfaces;
using Logistics.Presentation.Views.AnaEkranlar;
using Logistics.Presentation.Views.Surecler;

namespace Logistics.Presentation.Controllers;

// Admin işlemlerini yöneten controller
public class AdminController
{
    private readonly ILogOkuyucu _logOkuyucu;
    private readonly LogView _logView = new();

    public AdminController(ILogOkuyucu logOkuyucu)
    {
        _logOkuyucu = logOkuyucu;
    }

    // Controller döngüyü yönetir
    public void PaneliBaslat(AdminView view)
    {
        while (true)
        {
            view.EkraniCiz();
            string secim = view.GirdiAl();

            if (secim == "0") break;
            if (secim == "1") { LoglariGoster(); continue; }
        }
    }

    //admin rolü için log view'ı yönetir.
    private void LoglariGoster()
    {
        var logIcerigi = _logOkuyucu.LoglariOku();
        if (string.IsNullOrWhiteSpace(logIcerigi))
            _logView.LogYokMesaji();
        else
            _logView.LoglariGoster(logIcerigi);
    }
}

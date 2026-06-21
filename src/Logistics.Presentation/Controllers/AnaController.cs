using Logistics.Presentation.Views;
using Logistics.Presentation.Views.AnaEkranlar;
using Logistics.Presentation.Views.Factories;

namespace Logistics.Presentation.Controllers;

// Ana sistem kontrolcüsü — Sistemi başlatır, Login alır, Factory'yi tetikler
// Controller View'ı bilir — View Controller'ı bilmez
public class AnaController
{
    //yardımcı view ve controller sınıfları
    private readonly SiparisController _siparisController;
    private readonly DepoController _depoController;
    private readonly KuryeController _kuryeController;
    private readonly AdminController _adminController;
    private readonly LoginView _loginView = new();

    // hangi factory sınıfının çalışacağını girdilerle eşleştiren map
    private readonly Dictionary<int, IKullaniciMenuFactory> _factoryMap;

    // hangi panelin çalışacağını girdilerle eşleştiren map
    private readonly Dictionary<int, Action<IKullaniciView>> _panelMap;

    public AnaController(
        SiparisController siparisController,
        DepoController depoController,
        KuryeController kuryeController,
        AdminController adminController)
    {
        _siparisController = siparisController;
        _depoController = depoController;
        _kuryeController = kuryeController;
        _adminController = adminController;

        // kullanıcıdan gelen rol girdisine int değerine göre oluşturulacak factory'i veren map.
        _factoryMap = new Dictionary<int, IKullaniciMenuFactory>
        {
            { 1, new MusteriMenuFactory() },
            { 2, new DepoYoneticisiMenuFactory() },
            { 3, new KuryeMenuFactory() },
            { 4, new AdminMenuFactory() }
        };

        // Her rol için Controller panelini başlatan delege
        _panelMap = new Dictionary<int, Action<IKullaniciView>>
        {
            { 1, v => _siparisController.PaneliBaslat((MusteriView)v) },
            { 2, v => _depoController.PaneliBaslat((DepoView)v) },
            { 3, v => _kuryeController.PaneliBaslat((KuryeView)v) },
            { 4, v => _adminController.PaneliBaslat((AdminView)v) }
        };
    }

    public void Baslat()
    {
        while (true)
        {
            _loginView.EkraniCiz();
            int secim = _loginView.RolSecimi();

            if (secim == 0)
            {
                Console.WriteLine("  Sistem kapatılıyor...");
                break;
            }

            if (!_factoryMap.TryGetValue(secim, out var factory))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Geçersiz seçim!");
                Console.ResetColor();
                Thread.Sleep(1000);
                continue;
            }

            //seçilen değere göre runtime zamanda view factory metodu oluşturulur.
            var view = factory.FactoryMethod();

            //burada seçilen değere göre o delege çalışır. ve view'ı başlatır
            _panelMap[secim](view);
        }
    }
}

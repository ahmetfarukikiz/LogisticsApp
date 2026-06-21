using Logistics.Domain.Entities.Kullanicilar;
using Logistics.Domain.Interfaces;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Resolvers;
using Logistics.Domain.Services;
using Logistics.Domain.Services.Bildirim;
using Logistics.Infrastructure.Bildirimler;
using Logistics.Infrastructure.Logging;
using Logistics.Infrastructure.Repositories;
using Logistics.Presentation.Controllers;
using Logistics.Presentation.Resolvers;

namespace Logistics.Presentation;

// Composition root dependency injectionlar ve sistemin başlatılması burdan yapılır
class Program
{
    static void Main(string[] args)
    {
        // Singleton Logger LogManager'a bağla
        LogManager.LoggeriBagla(FileLogger.GetInstance());

        // Repositories (In-Memory veriler RAM'de tutulur)
        IUrunRepository urunRepository = new UrunRepository();
        IKullaniciRepository kullaniciRepository = new KullaniciRepository();
        ISiparisRepository siparisRepository = new SiparisRepository();

        // Observer Pattern — Publisher + Subscribers
        var publisher = new Publisher();
        var satinAlmaBirimi = new SatinAlmaBirimi();
        var depoYoneticisi = kullaniciRepository.TumKullanicilariGetir()
            .OfType<DepoYoneticisi>().FirstOrDefault();

        publisher.Attach(satinAlmaBirimi);
        if (depoYoneticisi != null) publisher.Attach(depoYoneticisi);

        // Resolvers (Dependency Inversion için arabirimler)
        // Strategy pattern'i uygulayabilmek ve switch case yapısından kurtulmak için
        // girdi çözücü olarak görev alacaklar
        IKargoResolver kargoResolver = new KargoResolver();
        IOdemeResolver odemeResolver = new OdemeResolver();

        // Domain servisleri
        var stokService = new StokService(urunRepository, publisher);
        var siparisService = new SiparisService(
            siparisRepository,
            stokService,
            kargoResolver,
            odemeResolver);

        // Controllerlar. 
        var siparisController = new SiparisController(siparisService, kullaniciRepository);
        var depoController = new DepoController(siparisService, stokService, kullaniciRepository);
        var kuryeController = new KuryeController(siparisService);

        //adminControllerın sadece okuma işlevine ihtiyacı var.
        // interface segregation
        ILogOkuyucu logOkuyucu = FileLogger.GetInstance();
        var adminController = new AdminController(logOkuyucu);

        // Ana Controller Factory ile View üretir, Controller'lara delege eder
        var anaController = new AnaController(
            siparisController, depoController, kuryeController, adminController);

        // Sistemi Başlat ve logla
        anaController.Baslat();
        LogManager.GetLogger().IslemiLogla("Sistem başlatıldı.");

    }
}

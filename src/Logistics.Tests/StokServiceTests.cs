using Logistics.Domain.Entities.Urunler;
using Logistics.Domain.Interfaces;
using Logistics.Domain.Services;
using Logistics.Domain.Services.Bildirim;
using Xunit;

namespace Logistics.Tests;

/// <summary>
/// StokService'in stok düşürüldüğünde sınır kontrolü yapıp 
/// doğru bildirimleri gönderdiğini test eder.
/// </summary>
public class StokServiceTests
{
    private class MockPublisher : ISubject
    {
        public string SonMesaj { get; private set; }
        public void Attach(IObserver observer) { }
        public void Detach(IObserver observer) { }
        public void Notify(string mesaj) => SonMesaj = mesaj;
    }

    private class MockRepo : IUrunRepository
    {
        public void UrunEkle(IUrun urun) { }
        public IUrun UrunGetir(int id) => null;
        public void UrunGuncelle(IUrun urun) { }
        public List<IUrun> TumUrunleriGetir() => new List<IUrun>();
    }

    private class MockLogger : ILogger
    {
        public void IslemiLogla(string mesaj) { }
    }

    public StokServiceTests()
    {
        LogManager.LoggeriBagla(new MockLogger());
    }

    [Fact]
    public void StokDusur_SinirAltinaDuserse_BildirimGondermeli()
    {
        // Arrange
        var publisher = new MockPublisher();
        var service = new StokService(new MockRepo(), publisher);
        // Stok: 6, Sınır: 5 -> Düştüğünde 5 olacak, sınırın ALTINDA değil.
        // Stok: 5, Sınır: 5 -> Düştüğünde 4 olacak, sınırın ALTINDA.
        var urun = new BasitUrun(5) { Isim = "Test", Sinir = 5, Id = 1, Fiyat = 10, Agirlik = 1 };

        // Act
        service.StokDusur(urun);

        // Assert
        Assert.Equal(4, urun.Stok);
        Assert.Contains("stoğu sınırın altına düştü", publisher.SonMesaj);
    }

    [Fact]
    public void StokDusur_SinirAltinaDusmezse_BildirimGondermemeli()
    {
        // Arrange
        var publisher = new MockPublisher();
        var service = new StokService(new MockRepo(), publisher);
        var urun = new BasitUrun(10) { Isim = "Test", Sinir = 5, Id = 1, Fiyat = 10, Agirlik = 1 };

        // Act
        service.StokDusur(urun);

        // Assert
        Assert.Equal(9, urun.Stok);
        Assert.Null(publisher.SonMesaj);
    }
}

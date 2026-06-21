using Logistics.Domain.Entities.Kullanicilar;
using Logistics.Domain.Services.OdemeManager;
using Xunit;

namespace Logistics.Tests;

/// <summary>
/// OdemeService'in ödeme stratejisini doğru tetiklediğini ve 
/// müşteri bakiyesini güncellediğini test eder.
/// </summary>
public class OdemeServiceTests
{
    // Test için basit bir ödeme stratejisi (Spy)
    private class MockOdemeStrategy : IOdemeStrategy
    {
        public bool OdemeAlindi { get; private set; }
        public void OdemeAl(decimal tutar) => OdemeAlindi = true;
    }

    [Fact]
    public void OdemeYap_BasariliOdeme_BakiyeyiDusurmeliVeTrueDonmeli()
    {
        // Arrange
        var musteri = new Musteri(1000m) { Id = 1, Isim = "Test" };
        var strategy = new MockOdemeStrategy();
        var service = new OdemeService(strategy);
        decimal tutar = 300m;

        // Act
        var result = service.OdemeYap(musteri, tutar);

        // Assert
        Assert.True(result.Basarili);
        Assert.Equal(700m, musteri.Bakiye);
        Assert.True(strategy.OdemeAlindi);
    }

    [Fact]
    public void OdemeYap_YetersizBakiye_HataMesajiDonmeliVeStratejiCagrilsaBileBakiyeDegismemeli()
    {
        // Arrange
        var musteri = new Musteri(100m) { Id = 1, Isim = "Test" };
        var strategy = new MockOdemeStrategy();
        var service = new OdemeService(strategy);
        decimal tutar = 500m;

        // Act
        var result = service.OdemeYap(musteri, tutar);

        // Assert
        Assert.False(result.Basarili);
        Assert.Contains("Yetersiz bakiye", result.Mesaj);
        Assert.Equal(100m, musteri.Bakiye); // Bakiye değişmemiş olmalı
    }
}

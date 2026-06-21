using System.Collections.Generic;
using Logistics.Domain.Entities;
using Logistics.Domain.Entities.Enums;
using Logistics.Domain.Services.KargoManager;
using Xunit;

namespace Logistics.Tests;

/// <summary>
/// KargoService'in adapter (IKargoTarget) kullanarak doğru fiyat hesapladığını test eder.
/// </summary>
public class KargoServiceTests
{
    private class MockKargoTarget : IKargoTarget
    {
        public decimal Fiyatlandir(double agirlik, int mesafe, List<HizmetTuru> hizmetler)
        {
            decimal temel = (decimal)(agirlik * mesafe);
            if (hizmetler.Contains(HizmetTuru.Sigortali)) temel += 50;
            return temel;
        }

        public int TakipNoUret() => 12345;
    }

    [Fact]
    public void KargoIslemleriniYap_HizmetYokken_SadeceTemelUcretHesaplamali()
    {
        // Arrange
        var target = new MockKargoTarget();
        var service = new KargoService(target);
        var detay = new KargoDetayDTO 
        { 
            Mesafe = 100, 
            Agirlik = 2, 
            Hizmetler = new List<HizmetTuru>() 
        };

        // Act
        var (ucret, takipNo) = service.KargoIslemleriniYap(detay);

        // Assert
        Assert.Equal(200m, ucret); // 100 * 2
        Assert.Equal(12345, takipNo);
    }

    [Fact]
    public void KargoIslemleriniYap_SigortaliHizmetVarsa_EkUcretEklemeli()
    {
        // Arrange
        var target = new MockKargoTarget();
        var service = new KargoService(target);
        var detay = new KargoDetayDTO 
        { 
            Mesafe = 100, 
            Agirlik = 2, 
            Hizmetler = new List<HizmetTuru> { HizmetTuru.Sigortali } 
        };

        // Act
        var (ucret, _) = service.KargoIslemleriniYap(detay);

        // Assert
        // Temel: 200, Sigorta: 50 -> Toplam 250
        Assert.Equal(250m, ucret);
    }
}

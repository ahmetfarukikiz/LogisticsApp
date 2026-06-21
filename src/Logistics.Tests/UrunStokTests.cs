using Logistics.Domain.Entities.Urunler;
using Xunit;

namespace Logistics.Tests;

/// <summary>
/// BasitUrun üzerindeki stok kapsülleme metotlarını test eder.
/// </summary>
public class UrunStokTests
{
    // Test için somut bir BasitUrun üreten yardımcı metot
    private static BasitUrun UrunOlustur(int baslangicStok, int sinir = 5)
        => new BasitUrun(baslangicStok) { Isim = "Test Ürünü", Id = 1, Sinir = sinir, Fiyat = 100, Agirlik = 1.0 };

    // --- StokDusur ---

    [Fact]
    public void StokDusur_StokluUrun_StoguBirAzaltir()
    {
        var urun = UrunOlustur(baslangicStok: 10);

        urun.StokDusur();

        Assert.Equal(9, urun.Stok);
    }

    [Fact]
    public void StokDusur_StokSifirken_InvalidOperationFirlatmali()
    {
        var urun = UrunOlustur(baslangicStok: 0);

        Assert.Throws<InvalidOperationException>(() => urun.StokDusur());
    }

    // --- StokArtir ---

    [Fact]
    public void StokArtir_HerZaman_StoguBirArtirir()
    {
        var urun = UrunOlustur(baslangicStok: 3);

        urun.StokArtir();

        Assert.Equal(4, urun.Stok);
    }

    // --- StokVarMi ---

    [Fact]
    public void StokVarMi_StokBirVeUzeri_DogruDonmeli()
    {
        var urun = UrunOlustur(baslangicStok: 1);
        Assert.True(urun.StokVarMi);
    }

    [Fact]
    public void StokVarMi_StokSifir_YanlisDonemli()
    {
        var urun = UrunOlustur(baslangicStok: 0);
        Assert.False(urun.StokVarMi);
    }

    // --- SinirinAltindaMi ---

    [Fact]
    public void SinirinAltindaMi_StokSinirdenDusuk_DogruDonemeli()
    {
        var urun = UrunOlustur(baslangicStok: 3, sinir: 5);
        Assert.True(urun.SinirinAltindaMi);
    }

    [Fact]
    public void SinirinAltindaMi_StokSinireEsit_YanlisDonemeli()
    {
        var urun = UrunOlustur(baslangicStok: 5, sinir: 5);
        // Sinire eşit → sınırın altında değil
        Assert.False(urun.SinirinAltindaMi);
    }
}

using Logistics.Domain.Entities.Kullanicilar;
using Xunit;

namespace Logistics.Tests;

/// <summary>
/// Musteri sınıfının bakiye kapsülleme metotlarını test eder.
/// BakiyeDus ve BakiyeEkle public set olmadığından domain metotları üzerinden test edilir.
/// </summary>
public class MusteriBakiyeTests
{
    // --- BakiyeDus ---

    [Fact]
    public void BakiyeDus_YeterliBakiye_BakiyeEksilmeli()
    {
        var musteri = new Musteri(1000m) { Id = 1, Isim = "Test Müşteri" };

        musteri.BakiyeDus(300m);

        Assert.Equal(700m, musteri.Bakiye);
    }

    [Fact]
    public void BakiyeDus_TamBakiye_SifiraInmeli()
    {
        var musteri = new Musteri(500m) { Id = 1, Isim = "Test Müşteri" };

        musteri.BakiyeDus(500m);

        Assert.Equal(0m, musteri.Bakiye);
    }

    [Fact]
    public void BakiyeDus_YetersizBakiye_InvalidOperationFirlatmali()
    {
        var musteri = new Musteri(100m) { Id = 1, Isim = "Test Müşteri" };

        Assert.Throws<InvalidOperationException>(() => musteri.BakiyeDus(200m));
    }

    [Fact]
    public void BakiyeDus_SifirTutar_ArgumentExceptionFirlatmali()
    {
        var musteri = new Musteri(500m) { Id = 1, Isim = "Test Müşteri" };

        Assert.Throws<ArgumentException>(() => musteri.BakiyeDus(0m));
    }

    [Fact]
    public void BakiyeDus_NegatifTutar_ArgumentExceptionFirlatmali()
    {
        var musteri = new Musteri(500m) { Id = 1, Isim = "Test Müşteri" };

        Assert.Throws<ArgumentException>(() => musteri.BakiyeDus(-50m));
    }

    // --- BakiyeEkle ---

    [Fact]
    public void BakiyeEkle_GecerliTutar_BakiyeArtmali()
    {
        var musteri = new Musteri(200m) { Id = 1, Isim = "Test Müşteri" };

        musteri.BakiyeEkle(150m);

        Assert.Equal(350m, musteri.Bakiye);
    }

    [Fact]
    public void BakiyeEkle_SifirTutar_ArgumentExceptionFirlatmali()
    {
        var musteri = new Musteri(200m) { Id = 1, Isim = "Test Müşteri" };

        Assert.Throws<ArgumentException>(() => musteri.BakiyeEkle(0m));
    }

    [Fact]
    public void BakiyeEkle_NegatifTutar_ArgumentExceptionFirlatmali()
    {
        var musteri = new Musteri(200m) { Id = 1, Isim = "Test Müşteri" };

        Assert.Throws<ArgumentException>(() => musteri.BakiyeEkle(-100m));
    }
}

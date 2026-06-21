using Logistics.Domain.Entities.Urunler;
using Xunit;

namespace Logistics.Tests;

/// <summary>
/// Composite Pattern: KarmasikUrun'un stok hesabının
/// alt parçaların minimum stoğuna dayandığını test eder.
/// </summary>
public class KarmasikUrunTests
{
    private static BasitUrun Parca(int stok)
        => new BasitUrun(stok) { Isim = $"Parça (stok={stok})", Id = stok, Sinir = 1, Fiyat = 100, Agirlik = 0.5 };

    // --- Stok (hesaplanan property) ---

    [Fact]
    public void Stok_AltParcaYokken_SifirDonemeli()
    {
        var urun = new KarmasikUrun { Id = 1, Isim = "Boş Set", Sinir = 1 };

        Assert.Equal(0, urun.Stok);
    }

    [Fact]
    public void Stok_AltParcalarDegisikStoklu_MinimumDonemeli()
    {
        var urun = new KarmasikUrun { Id = 1, Isim = "Toplama Bilgisayar", Sinir = 1 };
        urun.ParcaEkle(Parca(stok: 5));   // CPU
        urun.ParcaEkle(Parca(stok: 3));   // RAM → minimum bu

        Assert.Equal(3, urun.Stok);
    }

    [Fact]
    public void Stok_TekAltParca_OnunStogundaOlmali()
    {
        var urun = new KarmasikUrun { Id = 1, Isim = "Tekli Set", Sinir = 1 };
        urun.ParcaEkle(Parca(stok: 7));

        Assert.Equal(7, urun.Stok);
    }

    // --- StokDusur (cascade) ---

    [Fact]
    public void StokDusur_TumAltParcalarinStogunu_BirAzaltmali()
    {
        var cpu = Parca(stok: 5);
        var ram = Parca(stok: 3);
        var urun = new KarmasikUrun { Id = 1, Isim = "Toplama Bilgisayar", Sinir = 1 };
        urun.ParcaEkle(cpu);
        urun.ParcaEkle(ram);

        urun.StokDusur();

        Assert.Equal(4, cpu.Stok);
        Assert.Equal(2, ram.Stok);
        // Karmaşık ürünün stoku da minimum olan (şimdi 2) olmalı
        Assert.Equal(2, urun.Stok);
    }

    [Fact]
    public void StokDusur_HerhangiBirParcaSifirda_InvalidOperationFirlatmali()
    {
        var cpu = Parca(stok: 5);
        var ram = Parca(stok: 0); // tükenmiş parça
        var urun = new KarmasikUrun { Id = 1, Isim = "Toplama Bilgisayar", Sinir = 1 };
        urun.ParcaEkle(cpu);
        urun.ParcaEkle(ram);

        Assert.Throws<InvalidOperationException>(() => urun.StokDusur());
    }

    // --- StokArtir (cascade) ---

    [Fact]
    public void StokArtir_TumAltParcalarinStogunu_BirArtirir()
    {
        var cpu = Parca(stok: 4);
        var ram = Parca(stok: 2);
        var urun = new KarmasikUrun { Id = 1, Isim = "Toplama Bilgisayar", Sinir = 1 };
        urun.ParcaEkle(cpu);
        urun.ParcaEkle(ram);

        urun.StokArtir();

        Assert.Equal(5, cpu.Stok);
        Assert.Equal(3, ram.Stok);
    }

    // --- FiyatHesapla ---

    [Fact]
    public void FiyatHesapla_AltParcalarToplaminiDonmeli()
    {
        var p1 = new BasitUrun(1) { Id = 1, Isim = "CPU",  Sinir = 1, Fiyat = 5000, Agirlik = 0.5 };
        var p2 = new BasitUrun(1) { Id = 2, Isim = "RAM",  Sinir = 1, Fiyat = 3000, Agirlik = 1.0 };
        var urun = new KarmasikUrun { Id = 1, Isim = "PC", Sinir = 1 };
        urun.ParcaEkle(p1);
        urun.ParcaEkle(p2);

        Assert.Equal(8000m, urun.FiyatHesapla());
    }

    // --- HataliMi (Composite logic) ---

    [Fact]
    public void HataliMi_AltParcalardanBiriHataliysa_TrueDonmeli()
    {
        var cpu = new BasitUrun(5) { Id = 1, Isim = "CPU", HataliMi = true }; // Hatalı parça
        var ram = new BasitUrun(5) { Id = 2, Isim = "RAM", HataliMi = false };
        var urun = new KarmasikUrun { Id = 1, Isim = "PC" };
        urun.ParcaEkle(cpu);
        urun.ParcaEkle(ram);

        Assert.True(urun.HataliMi);
    }

    [Fact]
    public void HataliMi_TumParcalarSaglamVeUrunSaglamsa_FalseDonmeli()
    {
        var cpu = new BasitUrun(5) { Id = 1, Isim = "CPU", HataliMi = false };
        var urun = new KarmasikUrun { Id = 1, Isim = "PC", HataliMi = false };
        urun.ParcaEkle(cpu);

        Assert.False(urun.HataliMi);
    }
}

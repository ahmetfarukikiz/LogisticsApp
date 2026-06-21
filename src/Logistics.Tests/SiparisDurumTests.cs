using Logistics.Domain.Entities.Siparisler;
using Logistics.Domain.Entities.Siparisler.States;
using Logistics.Domain.Entities.Urunler;
using Xunit;

namespace Logistics.Tests;

/// <summary>
/// State Pattern: Siparis nesnesinin durum geçişlerini ve
/// her durumda doğru davranışı sergilediğini test eder.
/// </summary>
public class SiparisDurumTests
{
    private static Siparis SiparisOlustur()
    {
        var urun = new BasitUrun(5) { Id = 1, Isim = "Test Ürünü", Sinir = 1, Fiyat = 100, Agirlik = 1.0 };
        return new Siparis(new Beklemede())
        {
            Id = 1,
            Urun = urun,
            OdenenTutar = 150m,
            KargoTakipNo = 9999,
            KargoUcreti = 50m
        };
    }

    // --- Başlangıç durumu ---

    [Fact]
    public void YeniSiparis_BaşlangictaBeklemedeDurumunda_OlmaliDir()
    {
        var siparis = SiparisOlustur();

        Assert.Equal("Beklemede", siparis.GuncelDurum());
    }

    [Fact]
    public void YeniSiparis_Beklemede_IptalEdilebilirOlmali()
    {
        var siparis = SiparisOlustur();

        Assert.True(siparis.IptalEdilebilirMi());
    }

    // --- Beklemede → Onaylandı ---

    [Fact]
    public void IleriGecis_Beklemedeyken_OnaylandiDurumunuAlarakIlerlemeli()
    {
        var siparis = SiparisOlustur();

        siparis.IleriDurumGecis();

        Assert.Equal("Onaylandı", siparis.GuncelDurum());
    }

    // --- Onaylandı → Hazırlanıyor ---

    [Fact]
    public void IleriGecis_Onaylandiiken_HazirlanivorDurumunuAlarakIlerlemeli()
    {
        var siparis = SiparisOlustur();
        siparis.IleriDurumGecis(); // Beklemede → Onaylandı

        siparis.IleriDurumGecis(); // Onaylandı → Hazırlanıyor

        Assert.Equal("Hazırlanıyor", siparis.GuncelDurum());
    }

    // --- Hazırlanıyor → Kargoda ---

    [Fact]
    public void IleriGecis_Hazirlanivoriken_KargodaDurumunuAlarakIlerlemeli()
    {
        var siparis = SiparisOlustur();
        siparis.IleriDurumGecis(); // → Onaylandı
        siparis.IleriDurumGecis(); // → Hazırlanıyor

        siparis.IleriDurumGecis(); // → Kargoda

        Assert.Equal("Kargoda", siparis.GuncelDurum());
    }

    // --- Kargoda → Teslim Edildi ---

    [Fact]
    public void IleriGecis_Kargodayken_TeslimEdildiDurumunuAlarakIlerlemeli()
    {
        var siparis = SiparisOlustur();
        siparis.IleriDurumGecis(); // → Onaylandı
        siparis.IleriDurumGecis(); // → Hazırlanıyor
        siparis.IleriDurumGecis(); // → Kargoda

        siparis.IleriDurumGecis(); // → Teslim Edildi

        Assert.Equal("Teslim Edildi", siparis.GuncelDurum());
    }

    // --- TeslimEdildi: final durum ---

    [Fact]
    public void TeslimEdildi_IleriGecis_DurumDegismemeli()
    {
        var siparis = SiparisOlustur();
        siparis.IleriDurumGecis(); // → Onaylandı
        siparis.IleriDurumGecis(); // → Hazırlanıyor
        siparis.IleriDurumGecis(); // → Kargoda
        siparis.IleriDurumGecis(); // → Teslim Edildi

        siparis.IleriDurumGecis(); // Final durum, etkisiz olmalı

        Assert.Equal("Teslim Edildi", siparis.GuncelDurum());
    }

    [Fact]
    public void TeslimEdildi_IptalEdilebilirMi_YanlisDonemeli()
    {
        var siparis = SiparisOlustur();
        siparis.IleriDurumGecis();
        siparis.IleriDurumGecis();
        siparis.IleriDurumGecis();
        siparis.IleriDurumGecis(); // → Teslim Edildi

        Assert.False(siparis.IptalEdilebilirMi());
    }

    // --- SiparistenVazgec (iptal) ---

    [Fact]
    public void SiparistenVazgec_Beklemedeyken_IptalEdildiDurumunuAlmali()
    {
        var siparis = SiparisOlustur();

        siparis.SiparistenVazgec();

        Assert.Equal("İptal Edildi", siparis.GuncelDurum());
    }

    [Fact]
    public void SiparistenVazgec_TeslimEdildiktenSonra_IadeEdildiDurumunuAlmali()
    {
        var siparis = SiparisOlustur();
        siparis.IleriDurumGecis(); // → Onaylandı
        siparis.IleriDurumGecis(); // → Hazırlanıyor
        siparis.IleriDurumGecis(); // → Kargoda
        siparis.IleriDurumGecis(); // → Teslim Edildi

        siparis.SiparistenVazgec();

        Assert.Equal("İade Edildi", siparis.GuncelDurum());
    }

    [Fact]
    public void HataliUrun_KargodaykenIleriGecilirse_IadeEdildiOlmali()
    {
        // Arrange
        var urun = new BasitUrun(5) { Id = 1, Isim = "Hatalı Ürün", HataliMi = true };
        var siparis = new Siparis(new Kargoda()) { Urun = urun };

        // Act
        siparis.IleriDurumGecis();

        // Assert
        Assert.Equal("İade Edildi", siparis.GuncelDurum());
    }
}

namespace Logistics.Presentation.Views.AnaEkranlar;

// Müşteri ana ekranı 
// Controller referansı yok — sadece property'lerden okur, ekrana basar, girdi alır
public class MusteriView : IKullaniciView
{
    // View kendi verisini bulmaz, bu property'ler Controller tarafından doldurulur (Push Model)
    public decimal Bakiye { get; set; }
    public string SiparisDurumu { get; set; }
    public bool UrunListesiAktif { get; set; }
    public bool VazgecAktif { get; set; }
    public string VazgecEtiketi { get; set; } // "İptal Et" veya "İade Et"
    public bool SonrakiSiparisAktif { get; set; }

    public void EkraniCiz()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║           MÜŞTERİ MENÜSÜ                ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();

        // Bakiye bilgisi Controller sağladı
        Console.WriteLine($"  Bakiye: {Bakiye:N2} TL");

        // Sipariş durumu Controller sağladı
        if (!string.IsNullOrEmpty(SiparisDurumu))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  Sipariş -> {SiparisDurumu}");
            Console.ResetColor();
        }

        Console.WriteLine();

        // Ürün Listesi seçeneği
        Console.WriteLine(UrunListesiAktif
            ? "  1. Ürün Listesi"
            : "  1. Ürün Listesi  x");

        // İptal/İade seçeneği
        Console.WriteLine(VazgecAktif
            ? $"  2. {VazgecEtiketi}"
            : $"  2. {VazgecEtiketi ?? "İptal Et"}  x");

        // Sonraki siparişe geç seçeneği
        Console.WriteLine(SonrakiSiparisAktif
            ? "  3. Sonraki Siparişe Geç"
            : "  3. Sonraki Siparişe Geç  x");

        Console.WriteLine("  0. Çıkış");
        Console.WriteLine();
    }

    // controller gerektiğinde bu metotu kullanıp kullanıcıdan girdi alır
    public string GirdiAl()
    {
        Console.Write("Seçiminiz: ");
        return Console.ReadLine();
    }
}

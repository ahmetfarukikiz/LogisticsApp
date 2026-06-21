using System;

namespace Logistics.Presentation.Views.AnaEkranlar;

// Depo Yöneticisi ana ekranı — GERÇEK Dumb View
public class DepoView : IKullaniciView
{
    public string SiparisDurumu { get; set; }
    public bool HazirlamaAktif { get; set; }
    public bool KargoyaVerAktif { get; set; }

    public void EkraniCiz()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║      DEPO YÖNETİCİSİ MENÜSÜ            ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();

        if (!string.IsNullOrEmpty(SiparisDurumu))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  Sipariş -> {SiparisDurumu}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Aktif sipariş yok");
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.WriteLine(HazirlamaAktif ? "  1. Hazırlamayı Başlat" : "  1. Hazırlamayı Başlat  x");
        Console.WriteLine(KargoyaVerAktif ? "  2. Kargoya Ver" : "  2. Kargoya Ver  x");
        Console.WriteLine("  3. Ürün Listesi");
        Console.WriteLine("  4. Bildirimler");
        Console.WriteLine("  0. Çıkış");
        Console.WriteLine();
    }

    public string GirdiAl()
    {
        Console.Write("Seçiminiz: ");
        return Console.ReadLine();
    }
}

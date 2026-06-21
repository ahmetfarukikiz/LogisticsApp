using System;

namespace Logistics.Presentation.Views.AnaEkranlar;

// Kurye ana ekranı — GERÇEK Dumb View
// Controller referansı YOK — sadece property'lerden okur, ekrana basar, girdi alır
public class KuryeView : IKullaniciView
{
    // View kendi verisini bulmaz, bu property'ler Controller tarafından doldurulur (Push Model)
    public string SiparisDurumu { get; set; }
    public bool TeslimAktifMi { get; set; }

    public void EkraniCiz()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║           KURYE MENÜSÜ                  ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();

        // Veriyi Controller sağladı, View sadece ekrana basıyor
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

        Console.WriteLine(TeslimAktifMi
            ? "  1. Teslim Et"
            : "  1. Teslim Et  x");

        Console.WriteLine("  0. Çıkış");
        Console.WriteLine();
    }

    // Sadece kullanıcının girdiği metni alır — hiçbir karar vermeden dışarı fırlatır
    public string GirdiAl()
    {
        Console.Write("Seçiminiz: ");
        return Console.ReadLine();
    }
}

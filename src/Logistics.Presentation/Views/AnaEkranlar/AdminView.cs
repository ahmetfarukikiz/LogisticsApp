using System;

namespace Logistics.Presentation.Views.AnaEkranlar;

// Admin ana ekranı — GERÇEK Dumb View
public class AdminView : IKullaniciView
{
    public void EkraniCiz()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║           ADMİN MENÜSÜ                  ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("  1. Logları Görüntüle");
        Console.WriteLine("  0. Çıkış");
        Console.WriteLine();
    }

    public string GirdiAl()
    {
        Console.Write("Seçiminiz: ");
        return Console.ReadLine();
    }
}

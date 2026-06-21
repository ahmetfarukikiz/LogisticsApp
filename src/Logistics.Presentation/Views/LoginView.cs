using System;

namespace Logistics.Presentation.Views;

// Ana giriş ekranı — Rol seçim menüsü
public class LoginView
{
    public void EkraniCiz()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║   AKILLI TEDARİK VE LOJİSTİK YÖNETİM   ║");
        Console.WriteLine("║              SİSTEMİ                    ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("  1. Müşteri");
        Console.WriteLine("  2. Depo Yöneticisi");
        Console.WriteLine("  3. Kurye");
        Console.WriteLine("  4. Admin");
        Console.WriteLine("  0. Sistemi Kapat");
        Console.WriteLine();
    }

    public int RolSecimi()
    {
        Console.Write("Rol seçiniz: ");
        var girdi = Console.ReadLine();

        if (int.TryParse(girdi, out int secim))
            return secim;

        return -1;
    }
}

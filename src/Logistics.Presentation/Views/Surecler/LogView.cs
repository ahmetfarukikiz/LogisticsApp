using System;

namespace Logistics.Presentation.Views.Surecler;

// Admin log görüntüleme ekranı — Dumb View
public class LogView
{
    public void LoglariGoster(string logIcerigi)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║           LOG KAYITLARI                 ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine(logIcerigi);
        Console.WriteLine();
        DevamMesaji();
    }

    public void LogYokMesaji()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  Henüz log kaydı bulunmuyor.");
        Console.ResetColor();
        DevamMesaji();
    }

    public void DevamMesaji()
    {
        Console.WriteLine("  Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
}

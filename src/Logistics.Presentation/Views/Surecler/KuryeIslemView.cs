using System;

namespace Logistics.Presentation.Views.Surecler;

// Kurye alt işlem ekranları — Dumb View
public class KuryeIslemView
{
    public void TeslimOnayMesaji(string eskiDurum, string yeniDurum)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Sipariş teslim edildi! ({eskiDurum} -> {yeniDurum})");
        Console.ResetColor();
        DevamMesaji();
    }

    public void HataAnimasyonuGoster()
    {
        System.Threading.Thread.Sleep(1500);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Ürün hatalıdır iade ediliyor...");
        Console.ResetColor();
        System.Threading.Thread.Sleep(2000);
    }

    public void SiparisYokMesaji()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  Aktif sipariş bulunmuyor.");
        Console.ResetColor();
        DevamMesaji();
    }

    public void GecersizIslemMesaji()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  x Bu işlem şu an yapılamaz. Sipariş 'Kargoda' durumunda olmalıdır.");
        Console.ResetColor();
        DevamMesaji();
    }

    public void DevamMesaji()
    {
        Console.WriteLine("  Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
}

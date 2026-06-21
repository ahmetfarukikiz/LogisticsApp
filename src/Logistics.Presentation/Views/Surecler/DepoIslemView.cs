using System;

namespace Logistics.Presentation.Views.Surecler;

// Depo yöneticisi alt işlem ekranları — Dumb View
public class DepoIslemView
{
    public void DurumGuncelleMesaji(string eskiDurum, string yeniDurum)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Sipariş durumu güncellendi: {eskiDurum} -> {yeniDurum}");
        Console.ResetColor();
        DevamMesaji();
    }

    public void BildirimleriGoster(IReadOnlyList<string> bildirimler)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║           BİLDİRİMLER                   ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        if (bildirimler == null || bildirimler.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Henüz bildirim bulunmuyor.");
            Console.ResetColor();
        }
        else
        {
            for (int i = 0; i < bildirimler.Count; i++)
            {
                Console.WriteLine($"  [{i + 1}] {bildirimler[i]}");
            }
        }

        Console.WriteLine();
        DevamMesaji();
    }

    public void UrunListesiGoster(List<(string Isim, int Stok, int Sinir)> urunler)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║         ÜRÜN STOK LİSTESİ               ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"  {"Ürün",-25} {"Stok",-10} {"Sınır",-10} {"Durum",-15}");
        Console.WriteLine("  " + new string('-', 60));

        foreach (var urun in urunler)
        {
            string durum = urun.Stok < urun.Sinir ? "⚠ Sınırın altında!" : "Normal";
            ConsoleColor renk = urun.Stok < urun.Sinir ? ConsoleColor.Red : ConsoleColor.White;

            Console.ForegroundColor = renk;
            Console.WriteLine($"  {urun.Isim,-25} {urun.Stok,-10} {urun.Sinir,-10} {durum,-15}");
            Console.ResetColor();
        }

        Console.WriteLine();
        DevamMesaji();
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
        Console.WriteLine("  x Bu işlem şu an yapılamaz.");
        Console.ResetColor();
        DevamMesaji();
    }

    public void DevamMesaji()
    {
        Console.WriteLine("  Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
}

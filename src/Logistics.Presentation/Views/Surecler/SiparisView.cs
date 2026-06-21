namespace Logistics.Presentation.Views.Surecler;

// Sipariş verme sürecindeki ekranlar — Dumb View
public class SiparisView
{
    public void UrunleriGoster(List<(int Id, string Isim, decimal Fiyat, int Stok, double Agirlik)> urunBilgileri)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║             ÜRÜN LİSTESİ                ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"  {"ID",-5} {"Ürün",-25} {"Fiyat",-15} {"Stok",-10} {"Ağırlık",-10}");
        Console.WriteLine("  " + new string('-', 65));

        foreach (var urun in urunBilgileri)
        {
            string stokDurum = urun.Stok > 0 ? urun.Stok.ToString() : "Tükendi";
            Console.WriteLine($"  {urun.Id,-5} {urun.Isim,-25} {urun.Fiyat,-15:N2} {stokDurum,-10} {urun.Agirlik,-10:N2} kg");
        }
        Console.WriteLine();
    }

    public int UrunSecimiAl()
    {
        Console.Write("Satın almak istediğiniz ürünün ID'sini giriniz (0: İptal): ");
        var girdi = Console.ReadLine();

        if (int.TryParse(girdi, out int secim))
            return secim;

        return -1;
    }

    public void KargoSecenekleriniGoster()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("  === KARGO SEÇİMİ ===");
        Console.ResetColor();
        Console.WriteLine("  1. Aras Kargo");
        Console.WriteLine("  2. Yurtiçi Kargo");
        Console.WriteLine("  3. Global Express");
        Console.WriteLine();
    }

    public int KargoSecimiAl()
    {
        Console.Write("Kargo firması seçiniz: ");
        var girdi = Console.ReadLine();

        if (int.TryParse(girdi, out int secim))
            return secim;

        return -1;
    }

    public void HizmetSecenekleriniGoster()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("  === EK HİZMETLER ===");
        Console.ResetColor();
        Console.WriteLine("  Birden fazla seçebilirsiniz, virgülle ayırın.");
        Console.WriteLine("  1. Sigortalı Gönderim");
        Console.WriteLine("  2. Kırılacak Eşya Koruması");
        Console.WriteLine("  0. Ek hizmet istemiyorum");
        Console.WriteLine();
    }

    public string HizmetSecimiAl()
    {
        Console.Write("Seçiminiz (örn: 1,2): ");
        return Console.ReadLine();
    }

    public void KargoDetayGoster(string kargoFirmasi, decimal kargoUcreti, int takipNo, decimal urunFiyati, decimal toplamTutar)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  === KARGO DETAYLARI ===");
        Console.ResetColor();
        Console.WriteLine($"  Kargo Firması  : {kargoFirmasi}");
        Console.WriteLine($"  Kargo Ücreti   : {kargoUcreti:N2} TL");
        Console.WriteLine($"  Takip Numarası : {takipNo}");
        Console.WriteLine($"  Ürün Fiyatı    : {urunFiyati:N2} TL");
        Console.WriteLine($"  Toplam Tutar   : {toplamTutar:N2} TL");
        Console.WriteLine();
    }

    public void OdemeSecenekleriniGoster()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("  === ÖDEME YÖNTEMİ SEÇİMİ ===");
        Console.ResetColor();
        Console.WriteLine("  1. Kredi Kartı");
        Console.WriteLine("  2. Havale");
        Console.WriteLine();
    }

    public int OdemeSecimiAl()
    {
        Console.Write("Ödeme yöntemi seçiniz: ");
        var girdi = Console.ReadLine();

        if (int.TryParse(girdi, out int secim))
            return secim;

        return -1;
    }

    public void OdemeOnayMesaji()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($" Ödeme Başarılı!");
        Console.ResetColor();
        DevamMesaji();
    }

    public void HataMesaji(string mesaj)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  x {mesaj}");
        Console.ResetColor();
        DevamMesaji();
    }

    public void DevamMesaji()
    {
        Console.WriteLine();
        Console.WriteLine("  Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
}

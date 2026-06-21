namespace Logistics.Domain.Entities.Kullanicilar;

public class Musteri : Kullanici
{
    // Bakiye dışarıdan doğrudan değiştirilemez, sadece domain metotları üzerinden
    public decimal Bakiye { get; private set; }

    public Musteri(decimal baslangicBakiye)
    {
        Bakiye = baslangicBakiye;
    }

    // Ödeme işlemi sırasında bakiyeyi düşürür
    public void BakiyeDus(decimal tutar)
    {
        if (tutar <= 0)
            throw new ArgumentException("Düşülecek tutar pozitif olmalıdır.");
        if (Bakiye < tutar)
            throw new InvalidOperationException($"Yetersiz bakiye! Bakiye: {Bakiye:N2} TL, Gerekli: {tutar:N2} TL");
        Bakiye -= tutar;
    }

    // İade/iptal işlemi sırasında bakiyeye ekler
    public void BakiyeEkle(decimal tutar)
    {
        if (tutar <= 0)
            throw new ArgumentException("Eklenecek tutar pozitif olmalıdır.");
        Bakiye += tutar;
    }
}

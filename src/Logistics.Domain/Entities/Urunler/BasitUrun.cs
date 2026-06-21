namespace Logistics.Domain.Entities.Urunler;

public class BasitUrun : UrunBase
{
    public decimal Fiyat { get; init; }
    public double Agirlik { get; init; }

    public BasitUrun(int baslangicStok = 0) : base(baslangicStok) { }

    public override decimal FiyatHesapla()
    {
        return Fiyat;
    }

    public override double AgirlikHesapla()
    {
        return Agirlik;
    }
}

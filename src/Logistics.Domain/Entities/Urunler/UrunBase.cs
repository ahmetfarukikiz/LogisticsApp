namespace Logistics.Domain.Entities.Urunler;

public abstract class UrunBase : IUrun
{
    public int Id { get; init; }
    public required string Isim { get; init; }
    public virtual int Stok { get; protected set; }
    public int Sinir { get; init; }
    public virtual bool HataliMi { get; init; }

    protected UrunBase(int baslangicStok = 0)
    {
        Stok = baslangicStok;
    }

    public abstract double AgirlikHesapla();
    public abstract decimal FiyatHesapla();

    public bool SinirinAltindaMi => Stok < Sinir;
    public bool StokVarMi => Stok > 0;

    //encapsulation metotları. kapsülleme
    public virtual void StokDusur()
    {
        if (Stok <= 0)
            throw new InvalidOperationException($"'{Isim}' ürününün stoğu zaten sıfır, düşürülemez.");
        Stok--;
    }

    public virtual void StokArtir()
    {
        Stok++;
    }
}

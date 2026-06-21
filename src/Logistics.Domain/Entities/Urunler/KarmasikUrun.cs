namespace Logistics.Domain.Entities.Urunler;

public class KarmasikUrun : UrunBase
{
    private readonly List<IUrun> _altParcalar = new();

    public KarmasikUrun() : base(0) { }

    // Karmaşık ürünün stoğu, en az sayıda olan parçasının stoğuna eşittir.
    // Eğer hiç parça yoksa stok 0'dır.
    public override int Stok => _altParcalar.Any() ? _altParcalar.Min(p => p.Stok) : 0;

    // Alt parçalardan herhangi biri hatalıysa veya ürünün kendisi hatalı işaretlendiyse hatalıdır.
    public override bool HataliMi
    {
        get => base.HataliMi || _altParcalar.Any(p => p.HataliMi);
        init => base.HataliMi = value;
    }

    //alt parçaların fiyatlarını toplayarak fiyatını hesaplar.
    public override decimal FiyatHesapla()
    {
        return _altParcalar.Sum(c => c.FiyatHesapla());
    }

    //alt parçaların ağırlıklarını toplayarak ağırlığını hesaplar.
    public override double AgirlikHesapla()
    {
        return _altParcalar.Sum(c => c.AgirlikHesapla());
    }

    public void ParcaEkle(IUrun component)
    {
        _altParcalar.Add(component);
    }

    public void ParcaCikar(IUrun component)
    {
        _altParcalar.Remove(component);
    }

    public override void StokDusur()
    {
        // Karmaşık ürünün kendi stoğunu düşürmüyoruz, çünkü Stok property'si parçalardan hesaplanıyor.
        // Sadece parçaların stoğunu düşürüyoruz.
        foreach (var parca in _altParcalar)
        {
            parca.StokDusur();
        }
    }

    public override void StokArtir()
    {
        // Parçaların stoğunu artırıyoruz.
        foreach (var parca in _altParcalar)
        {
            parca.StokArtir();
        }
    }
}

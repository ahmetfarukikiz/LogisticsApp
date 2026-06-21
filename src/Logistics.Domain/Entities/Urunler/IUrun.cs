namespace Logistics.Domain.Entities.Urunler;

//composite pattern arayüzü 
//karmaşık ve basit ürünler bu arayüzü implemente eder
public interface IUrun
{
    double AgirlikHesapla();
    decimal FiyatHesapla();
    int Stok { get; }
    bool SinirinAltindaMi { get; }
    bool StokVarMi { get; }
    bool HataliMi { get; }
    void StokDusur();
    void StokArtir();
}

using Logistics.Domain.Entities.Urunler;

namespace Logistics.Domain.Interfaces;

public interface IUrunRepository
{
    List<IUrun> TumUrunleriGetir();
    IUrun UrunGetir(int id);
    void UrunEkle(IUrun urun);
    void UrunGuncelle(IUrun urun);
}

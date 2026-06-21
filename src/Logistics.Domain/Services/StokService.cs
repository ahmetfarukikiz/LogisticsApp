using Logistics.Domain.Entities.Urunler;
using Logistics.Domain.Interfaces;
using Logistics.Domain.Services.Bildirim;

namespace Logistics.Domain.Services;

// Stok yönetimi: stok düşürme/artırma, sınır kontrolü ve bildirim
public class StokService
{
    //tüm veriler tamamen bir arayüz üzerinden çekilir bu sayede mock data veya veritabanı
    //bağlansa bile domain katmanı etkilenmez
    private readonly IUrunRepository _urunRepository;
    private readonly ISubject _publisher;

    public StokService(IUrunRepository urunRepository, ISubject publisher)
    {
        _urunRepository = urunRepository;
        _publisher = publisher;
    }

    public List<IUrun> UrunleriGetir()
    {
        return _urunRepository.TumUrunleriGetir();
    }

    public IUrun UrunGetir(int id)
    {
        return _urunRepository.UrunGetir(id);
    }

    //sipariş onaylandıktan sonra çağrılır
    public void StokDusur(IUrun urun)
    {
        var urunBase = (UrunBase)urun;
        if (!urun.StokVarMi)
            return;

        // Kapsülleme: stok değeri doğrudan değil, nesnenin kendi metodu üzerinden değişir
        urunBase.StokDusur();
        LogManager.GetLogger().IslemiLogla($"Stok düşürüldü: {urunBase.Isim} -> Kalan stok: {urunBase.Stok}");

        if (urun.SinirinAltindaMi)
        {
            _publisher.Notify($"UYARI: '{urunBase.Isim}' ürününün stoğu sınırın altına düştü! (Stok: {urunBase.Stok}, Sınır: {urunBase.Sinir})");
        }
    }

    // İade/iptal sonrası çağırılır
    public void StokArtir(IUrun urun)
    {
        var urunBase = (UrunBase)urun;
        // Kapsülleme: stok değeri doğrudan değil, nesnenin kendi metodu üzerinden değişir
        urunBase.StokArtir();
        LogManager.GetLogger().IslemiLogla($"Stok artırıldı: {urunBase.Isim} -> Kalan stok: {urunBase.Stok}");
    }
}

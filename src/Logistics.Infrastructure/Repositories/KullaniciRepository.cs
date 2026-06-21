using Logistics.Domain.Entities.Kullanicilar;
using Logistics.Domain.Interfaces.Repositories;

namespace Logistics.Infrastructure.Repositories;

public class KullaniciRepository : IKullaniciRepository
{
    private readonly List<Kullanici> _kullanicilar;

    public KullaniciRepository()
    {
        _kullanicilar = new List<Kullanici>
        {
            new Admin { Id = 1, Isim = "Admin" },
            new Musteri(50000m) { Id = 2, Isim = "Müşteri" },//başlangıç bakiyesi 50000tl
            new DepoYoneticisi { Id = 3, Isim = "Depo" },
            new Kurye { Id = 4, Isim = "Kurye" }
        };
    }

    public Kullanici KullaniciGetir(int id)
    {
        return _kullanicilar.FirstOrDefault(k => k.Id == id);
    }

    public List<Kullanici> TumKullanicilariGetir()
    {
        return _kullanicilar;
    }
}

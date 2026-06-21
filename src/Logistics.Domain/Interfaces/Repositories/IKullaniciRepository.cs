using Logistics.Domain.Entities.Kullanicilar;

namespace Logistics.Domain.Interfaces.Repositories;

public interface IKullaniciRepository
{
    Kullanici KullaniciGetir(int id);
    List<Kullanici> TumKullanicilariGetir();
}

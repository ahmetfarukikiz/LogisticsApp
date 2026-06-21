using Logistics.Domain.Entities.Siparisler;

namespace Logistics.Domain.Interfaces.Repositories;

public interface ISiparisRepository
{
    Siparis SiparisGetir();
    void SiparisAyarla(Siparis siparis);
    void SiparisSil();
}

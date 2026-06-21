using Logistics.Domain.Entities.Siparisler;
using Logistics.Domain.Interfaces.Repositories;

namespace Logistics.Infrastructure.Repositories;

public class SiparisRepository : ISiparisRepository
{
    private Siparis _aktifSiparis;

    public Siparis SiparisGetir()
    {
        return _aktifSiparis;
    }

    public void SiparisAyarla(Siparis siparis)
    {
        _aktifSiparis = siparis;
    }

    public void SiparisSil()
    {
        _aktifSiparis = null;
    }
}

using Logistics.Domain.Entities.Enums;
using Logistics.Domain.Services.KargoManager;
using Logistics.Infrastructure.KargoAPIs;

namespace Logistics.Infrastructure.KargoAdaptors;

public class YurtIciAdaptor : IKargoTarget
{
    private readonly YurtIciApi _kargo = new YurtIciApi();

    public decimal Fiyatlandir(double agirlik, int mesafe, List<HizmetTuru> hizmetler)
    {
        return (decimal)_kargo.UcretHesapla(agirlik, mesafe, hizmetler);
    }

    public int TakipNoUret()
    {
        return _kargo.TakipNoUret();
    }
}

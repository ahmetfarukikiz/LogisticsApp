using System.Collections.Generic;
using Logistics.Domain.Entities.Enums;
using Logistics.Domain.Services.KargoManager;
using Logistics.Infrastructure.KargoAPIs;

namespace Logistics.Infrastructure.KargoAdaptors;

public class ArasAdaptor : IKargoTarget
{
    private readonly ArasApi _kargo = new ArasApi();

    public decimal Fiyatlandir(double agirlik, int mesafe, List<HizmetTuru> hizmetler)
    {
        return (decimal)_kargo.FiyatiBelirle(agirlik, mesafe, hizmetler);
    }

    public int TakipNoUret()
    {
        return _kargo.NoUretici();
    }
}

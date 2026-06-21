using System.Collections.Generic;
using Logistics.Domain.Entities.Enums;
using Logistics.Domain.Services.KargoManager;
using Logistics.Infrastructure.KargoAPIs;

namespace Logistics.Infrastructure.KargoAdaptors;

public class GlobalExpressAdaptor : IKargoTarget
{
    private readonly GlobalExpressApi _kargo = new GlobalExpressApi();

    public decimal Fiyatlandir(double agirlik, int mesafe, List<HizmetTuru> hizmetler)
    {
        return (decimal)_kargo.CalculateProductPrice(agirlik, mesafe, hizmetler);
    }

    public int TakipNoUret()
    {
        return _kargo.ProduceID();
    }
}

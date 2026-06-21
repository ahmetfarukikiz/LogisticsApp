using System.Collections.Generic;
using Logistics.Domain.Entities.Enums;

namespace Logistics.Domain.Services.KargoManager;

public interface IKargoTarget
{
    decimal Fiyatlandir(double agirlik, int mesafe, List<HizmetTuru> hizmetler);
    int TakipNoUret();
}

using System.Collections.Generic;
using Logistics.Domain.Entities.Enums;

namespace Logistics.Infrastructure.KargoAPIs;

public class ArasApi
{
    public double FiyatiBelirle(double agirlik, int mesafe, List<HizmetTuru> hizmetler)
    {
        double ucret = agirlik * 1.2 + mesafe * 0.5;
        if (hizmetler.Contains(HizmetTuru.Sigortali)) ucret += 50;
        return ucret;
    }

    public short NoUretici()
    {
        return (short)new System.Random().Next(1000, 9999);
    }
}

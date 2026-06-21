using System.Collections.Generic;
using Logistics.Domain.Entities.Enums;

namespace Logistics.Infrastructure.KargoAPIs;

public class GlobalExpressApi
{
    public float CalculateProductPrice(double agirlik, int mesafe, List<HizmetTuru> hizmetler)
    {
        float price = (float)(agirlik * 2.0 + mesafe * 1.0);
        
        if (hizmetler.Contains(HizmetTuru.Sigortali)) price += 50f;
        if (hizmetler.Contains(HizmetTuru.KirilacakEsya)) price += 30f;
        
        return price;
    }

    public int ProduceID()
    {
        return new System.Random().Next(100000, 999999);
    }
}

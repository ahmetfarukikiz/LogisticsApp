using System.Collections.Generic;
using Logistics.Domain.Entities.Enums;

namespace Logistics.Infrastructure.KargoAPIs;

public class YurtIciApi
{
    public double UcretHesapla(double agirlik, int mesafe, List<HizmetTuru> hizmetler)
    {
        double ucret = agirlik * 1.5 + mesafe * 0.4;
        if (hizmetler.Contains(HizmetTuru.KirilacakEsya)) ucret += 30;
        return ucret;
    }

    public int TakipNoUret()
    {
        return new System.Random().Next(10000, 99999);
    }
}
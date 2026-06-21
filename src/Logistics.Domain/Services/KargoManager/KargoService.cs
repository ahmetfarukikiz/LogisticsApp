using System.Collections.Generic;
using Logistics.Domain.Entities;
using Logistics.Domain.Entities.Enums;

namespace Logistics.Domain.Services.KargoManager;

// Kargo işlemlerini yöneten servis.
// Sequence Diagram: KargoIslemleriniYap(KargoDetayDTO) -> (KargoUcret, TakipNo)
public class KargoService
{
    private readonly IKargoTarget _kargo;

    public KargoService(IKargoTarget kargo)
    {
        _kargo = kargo;
    }

    // Sequence Diagram'daki tam imza: adapter pattern ile seçilen kargoApi'ye istek atılır
    // ve ortak arayüz üzerinden hesaplanır. (KargoUcret, TakipNo) tuple döndürüyor.
    public (decimal KargoUcreti, int TakipNo) KargoIslemleriniYap(KargoDetayDTO kargoDetay)
    {
        decimal kargoUcreti = _kargo.Fiyatlandir(kargoDetay.Agirlik, kargoDetay.Mesafe, kargoDetay.Hizmetler);
        int takipNo = _kargo.TakipNoUret();
        return (kargoUcreti, takipNo);
    }
}

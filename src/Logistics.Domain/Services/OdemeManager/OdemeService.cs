using Logistics.Domain.Entities.Kullanicilar;

namespace Logistics.Domain.Services.OdemeManager;

// Ödeme işlemlerini yöneten servis.
// Sequence Diagram: OdemeYap(OdemeTuru, SonTutar)
public class OdemeService
{
    private readonly IOdemeStrategy _odemeStrategy;

    public OdemeService(IOdemeStrategy odemeStrategy)
    {
        _odemeStrategy = odemeStrategy;
    }

    // Sequence Diagram imzası: OdemeYap(OdemeTuru, SonTutar)
    // Bakiye kontrolü, ödeme alma ve loglama — tümü bu servisin sorumluluğu.
    public (bool Basarili, string Mesaj) OdemeYap(Musteri musteri, decimal tutar)
    {
        try
        {
            // Kapsülleme: bakiye kontrolü ve düşümü Musteri nesnesinin kendi kuralları içinde
            _odemeStrategy.OdemeAl(tutar);
            musteri.BakiyeDus(tutar);
            return (true, "Ödeme başarılı.");
        }
        catch (InvalidOperationException ex)
        {
            return (false, ex.Message);
        }
    }
}

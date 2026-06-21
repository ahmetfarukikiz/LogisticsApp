using Logistics.Domain.Services.OdemeManager;

namespace Logistics.Domain.Interfaces.Resolvers;

public interface IOdemeResolver
{
    //kullanýcýdan alýnan int þeklindeki girdileri map'e göre çözerek üretilecek sýnýfý döndürür
    IOdemeStrategy Coz(int odemeTuru);
    //gelen int deðerin hangi isme karþýlýk geldiðini belirler
    string IsimGetir(int odemeTuru);

    //gelen int deðerin map'te karþýlýðý olup olmadýðýný döndürür
    bool GecerliMi(int odemeTuru);
}

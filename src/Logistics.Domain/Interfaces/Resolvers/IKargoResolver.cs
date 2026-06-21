using Logistics.Domain.Services.KargoManager;

namespace Logistics.Domain.Interfaces.Resolvers;

public interface IKargoResolver
{
    //kullanýcýdan alýnan int þeklindeki girdileri map'e göre çözerek üretilecek adapter sýnýfýný döndürür

    IKargoTarget Coz(int kargoTipi);

    //kullanýcýdan gelen int deðerin hangi isme karþýlýk geldiðini döndürür
    string IsimGetir(int kargoTipi);

    //gelen int deðerin map'te karþýlýðý olup olmadýðýný döndürür
    bool GecerliMi(int kargoTipi);
}

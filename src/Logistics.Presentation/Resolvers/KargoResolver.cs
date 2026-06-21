using Logistics.Domain.Interfaces.Resolvers;
using Logistics.Domain.Services.KargoManager;
using Logistics.Infrastructure.KargoAdaptors;

namespace Logistics.Presentation.Resolvers;



public class KargoResolver : IKargoResolver
{
    //viewdaki ekrana 1 2 3 şeklinde alınan girdiyi burda adaptor sınıflarıyla eşleştirir
    private readonly Dictionary<int, Func<IKargoTarget>> _cozuculer = new()
    {
        { 1, () => new ArasAdaptor() },
        { 2, () => new YurtIciAdaptor() },
        { 3, () => new GlobalExpressAdaptor() }
    };

    private readonly Dictionary<int, string> _isimler = new()
    {
        { 1, "Aras Kargo" }, { 2, "Yurtiçi Kargo" }, { 3, "Global Express" }
    };

    public bool GecerliMi(int kargoTipi) => _cozuculer.ContainsKey(kargoTipi);
    //alınan girdilerin hangi adaptor sınıfına karşılık geleceğini ve üretileceğini belirler.
    public IKargoTarget Coz(int kargoTipi) => _cozuculer[kargoTipi]();

    //lazım olduğunda isimleriyle önceden maplenmiş değerlerden isimlerini bulur.
    public string IsimGetir(int kargoTipi) => _isimler[kargoTipi];
}

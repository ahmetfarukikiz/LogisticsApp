using Logistics.Domain.Interfaces.Resolvers;
using Logistics.Domain.Services.OdemeManager;

namespace Logistics.Presentation.Resolvers;

public class OdemeResolver : IOdemeResolver
{
    //alınan girdiye göre hangi sınıfın üretileceğini mapler.
    private readonly Dictionary<int, Func<IOdemeStrategy>> _cozuculer = new()
    {
        { 1, () => new KrediKarti() },
        { 2, () => new Havale() }
    };

    //isimleri aynı girdilerle mapler
    private readonly Dictionary<int, string> _isimler = new()
    {
        { 1, "Kredi Kartı" }, { 2, "Havale" }
    };

    public bool GecerliMi(int odemeTuru) => _cozuculer.ContainsKey(odemeTuru);
    //alınan girdi geldiğinde bu girdi sayısının hangi sınıfa karşılık geldiğini çözer
    public IOdemeStrategy Coz(int odemeTuru) => _cozuculer[odemeTuru]();

    //gelen int değerine bakarak hangi isme karşılık geldiğini bulur.
    public string IsimGetir(int odemeTuru) => _isimler[odemeTuru];
}

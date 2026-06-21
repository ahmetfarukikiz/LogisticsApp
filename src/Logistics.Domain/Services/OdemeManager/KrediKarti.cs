namespace Logistics.Domain.Services.OdemeManager;

public class KrediKarti : IOdemeStrategy
{
    public void OdemeAl(decimal tutar)
    {
        LogManager.GetLogger().IslemiLogla($"STRATEGY Kredi Kartı ile {tutar} TL ödeme alındı.");
    }
}

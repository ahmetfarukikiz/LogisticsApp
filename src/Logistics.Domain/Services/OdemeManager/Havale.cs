namespace Logistics.Domain.Services.OdemeManager;

public class Havale : IOdemeStrategy
{
    public void OdemeAl(decimal tutar)
    {
        LogManager.GetLogger().IslemiLogla($"STRATEGY Havale ile {tutar} TL ödeme alındı.");
    }
}

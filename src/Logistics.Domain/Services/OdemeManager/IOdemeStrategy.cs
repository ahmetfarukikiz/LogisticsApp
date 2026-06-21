namespace Logistics.Domain.Services.OdemeManager;

public interface IOdemeStrategy
{
    //runtime zamanda strategy'e göre gösterilecek mesajý belirleyen fonksiyon
    void OdemeAl(decimal tutar);
}

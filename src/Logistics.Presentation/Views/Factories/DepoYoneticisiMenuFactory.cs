using Logistics.Presentation.Views.AnaEkranlar;

namespace Logistics.Presentation.Views.Factories;

// Factory Method — Depo Yöneticisi View'ını üretir
public class DepoYoneticisiMenuFactory : IKullaniciMenuFactory
{
    public IKullaniciView FactoryMethod()
    {
        return new DepoView();
    }
}

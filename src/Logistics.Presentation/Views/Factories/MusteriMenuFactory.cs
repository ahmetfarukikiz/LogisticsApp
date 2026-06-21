using Logistics.Presentation.Views.AnaEkranlar;

namespace Logistics.Presentation.Views.Factories;

// Factory Method Müşteri View'ını üretir
public class MusteriMenuFactory : IKullaniciMenuFactory
{
    public IKullaniciView FactoryMethod()
    {
        return new MusteriView();
    }
}

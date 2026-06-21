using Logistics.Presentation.Views.AnaEkranlar;

namespace Logistics.Presentation.Views.Factories;

// Factory Method — Admin View'ını üretir
public class AdminMenuFactory : IKullaniciMenuFactory
{
    public IKullaniciView FactoryMethod()
    {
        return new AdminView();
    }
}

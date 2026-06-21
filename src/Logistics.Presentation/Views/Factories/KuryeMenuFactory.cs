using Logistics.Presentation.Views.AnaEkranlar;

namespace Logistics.Presentation.Views.Factories;

// Factory Method — Kurye View'ını üretir
public class KuryeMenuFactory : IKullaniciMenuFactory
{
    public IKullaniciView FactoryMethod()
    {
        return new KuryeView();
    }
}

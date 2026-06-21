using Logistics.Presentation.Views.AnaEkranlar;

namespace Logistics.Presentation.Views.Factories;

// Factory Method Pattern — View üretimi için arayüz
public interface IKullaniciMenuFactory
{
    IKullaniciView FactoryMethod();
}

namespace Logistics.Presentation.Views.AnaEkranlar;

// Factory Method Pattern ile üretilen View'ların ortak arayüzü
// Viewlar controller referansı tutmaz controller view'ı yönetir.
public interface IKullaniciView
{
    void EkraniCiz(); // controller gerektiğinde bu metodu çağırarak ilgili menüyü çizer
    string GirdiAl(); // Saf girdi döner hiçbir karar vermez controller kullanır
}

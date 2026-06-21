using Logistics.Domain.Services.Bildirim;

namespace Logistics.Domain.Entities.Kullanicilar;

public class DepoYoneticisi : Kullanici, IObserver
{
    private List<string> _bildirimler = new List<string>();

    public IReadOnlyList<string> GetBildirimler()
    {
        return _bildirimler.AsReadOnly();
    }

    public void Update(string mesaj)
    {
        _bildirimler.Add(mesaj);
    }
}

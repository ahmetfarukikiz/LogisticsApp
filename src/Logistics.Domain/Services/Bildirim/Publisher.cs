namespace Logistics.Domain.Services.Bildirim;

public class Publisher : ISubject
{
    private readonly List<IObserver> _subscribers = new();

    //abone eden metot
    public void Attach(IObserver observer)
    {
        if (!_subscribers.Contains(observer))
        {
            _subscribers.Add(observer);
        }
    }
    //abonelikten çýkaran metot
    public void Detach(IObserver observer)
    {
        _subscribers.Remove(observer);
    }

    //tüm abonelerine güncelleme sinyali veren metot
    public void Notify(string mesaj)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Update(mesaj);
        }
    }
}

using System;
using Logistics.Domain.Services.Bildirim;

namespace Logistics.Infrastructure.Bildirimler;

public class SatinAlmaBirimi : IObserver
{
    public void Update(string mesaj)
    {
        Console.WriteLine($"[Email Gönderildi] Satın Alma Birimine Bildirim: {mesaj}");
    }
}

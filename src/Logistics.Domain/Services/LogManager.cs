using Logistics.Domain.Interfaces;

namespace Logistics.Domain.Services;

//logger wrapper class //domain katmanı hangi loglayicinin calistigini bilmez
//basta logger baglanir ve o kullanilir
public static class LogManager
{
    private static ILogger _logger;

    //program.cs'te Infrastructure katmanından dependency injection
    //ile gönderilecek logger sınıfını bağlayan metot
    public static void LoggeriBagla(ILogger logger)
    {
        _logger = logger;
    }

    public static ILogger GetLogger()
    {
        if (_logger == null)
            throw new Exception("Logger henuz baglanmadi!");

        return _logger;
    }
}
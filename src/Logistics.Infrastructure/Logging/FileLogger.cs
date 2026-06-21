using Logistics.Domain.Interfaces;

namespace Logistics.Infrastructure.Logging;

// Singleton Pattern — Sistemde tek bir FileLogger nesnesi bulunur
//Interface Segregation Principle kullanılmıştır.
public sealed class FileLogger : ILogger, ILogOkuyucu
{
    private static volatile FileLogger _instance;
    private static readonly object _lock = new object();
    private readonly string _logFilePath = "log.txt";

    private FileLogger()
    {
        // Private constructor for Singleton
    }

    public static FileLogger GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new FileLogger();
                }
            }
        }
        return _instance;
    }

    public void IslemiLogla(string mesaj)
    {
        lock (_lock)
        {
            File.AppendAllText(_logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mesaj}{Environment.NewLine}");
        }
    }

    public string LoglariOku()
    {
        if (!File.Exists(_logFilePath))
            return null;

        return File.ReadAllText(_logFilePath);
    }
}

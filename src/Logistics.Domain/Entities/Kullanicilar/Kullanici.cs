namespace Logistics.Domain.Entities.Kullanicilar;

public abstract class Kullanici
{
    // init: kimlik bilgileri nesnesi oluştuktan sonra değişmemesi için
    public int Id { get; init; }
    public string Isim { get; init; } = string.Empty;
}

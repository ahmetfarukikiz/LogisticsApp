using Logistics.Domain.Entities.Enums;

namespace Logistics.Domain.Entities;

public class KargoDetayDTO
{
    public int Mesafe { get; set; }
    public double Agirlik { get; set; }
    public List<HizmetTuru> Hizmetler { get; set; } = new();
}

namespace Logistics.Domain.Entities.Siparisler;

//siparisOlustur metodundan dönen deðerleri taþýyan data transfer object
public class SiparisOzetiDTO
{
    public decimal KargoUcreti { get; set; }
    public int TakipNo { get; set; }
    public decimal ToplamTutar { get; set; }
    public string KargoIsmi { get; set; }
    public string Hata { get; set; }

    public bool Basarili => string.IsNullOrEmpty(Hata);
}

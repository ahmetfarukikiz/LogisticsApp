namespace Logistics.Domain.Entities.Siparisler.States;

public class IptalEdildi : IDurum
{
    private Siparis _siparis;

    public bool IptalEdilebilirMi => true;

    public void SetSiparis(Siparis sp)
    {
        _siparis = sp;
    }

    public void SiparistenVazgec()
    {
        // Zaten iptal edildi — işlem yapılmaz.
    }

    public void IleriGecis()
    {
        // Final durum — ileri geçiş yok.
    }

    public string GuncelDurum()
    {
        return "İptal Edildi";
    }
}

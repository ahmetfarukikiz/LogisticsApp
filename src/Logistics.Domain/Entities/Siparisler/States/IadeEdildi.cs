namespace Logistics.Domain.Entities.Siparisler.States;

public class IadeEdildi : IDurum
{
    private Siparis _siparis;

    public bool IptalEdilebilirMi => false;

    public void SetSiparis(Siparis sp)
    {
        _siparis = sp;
    }

    public void SiparistenVazgec()
    {
        // Zaten iade edildi — işlem yapılmaz.
    }

    public void IleriGecis()
    {
        // Final durum — ileri geçiş yok.
    }

    public string GuncelDurum()
    {
        return "İade Edildi";
    }
}

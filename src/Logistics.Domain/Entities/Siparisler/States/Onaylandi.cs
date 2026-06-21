namespace Logistics.Domain.Entities.Siparisler.States;

public class Onaylandi : IDurum
{
    private Siparis _siparis;

    public bool IptalEdilebilirMi => true;

    public void SetSiparis(Siparis sp)
    {
        _siparis = sp;
    }

    public void SiparistenVazgec()
    {
        _siparis.DurumDegistir(new IptalEdildi());
    }

    public void IleriGecis()
    {
        _siparis.DurumDegistir(new Hazirlaniyor());
    }

    public string GuncelDurum()
    {
        return "Onaylandı";
    }
}

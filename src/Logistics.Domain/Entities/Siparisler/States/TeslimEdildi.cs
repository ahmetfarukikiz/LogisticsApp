namespace Logistics.Domain.Entities.Siparisler.States;

public class TeslimEdildi : IDurum
{
    private Siparis _siparis;

    public bool IptalEdilebilirMi => false;

    public void SetSiparis(Siparis sp)
    {
        _siparis = sp;
    }

    public void SiparistenVazgec()
    {
        _siparis.DurumDegistir(new IadeEdildi());
    }

    public void IleriGecis()
    {
        // Final durum — ileri geçiş yok.
    }

    public string GuncelDurum()
    {
        return "Teslim Edildi";
    }
}

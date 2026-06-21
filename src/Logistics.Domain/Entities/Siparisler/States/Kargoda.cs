namespace Logistics.Domain.Entities.Siparisler.States;

public class Kargoda : IDurum
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
        if (_siparis.Urun.HataliMi)
        {
            _siparis.DurumDegistir(new IadeEdildi());
        }
        else
        {
            _siparis.DurumDegistir(new TeslimEdildi());
        }
    }

    public string GuncelDurum()
    {
        return "Kargoda";
    }
}

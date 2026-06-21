using Logistics.Domain.Entities.Siparisler.States;
using Logistics.Domain.Entities.Urunler;

namespace Logistics.Domain.Entities.Siparisler;

public class Siparis
{
    public int Id { get; init; }
    public IUrun Urun { get; init; }
    public decimal OdenenTutar { get; init; }
    public int KargoTakipNo { get; init; }
    public decimal KargoUcreti { get; init; }

    private IDurum _durum;

    public Siparis(IDurum baslangicDurumu)
    {
        DurumDegistir(baslangicDurumu);
    }

    public void DurumDegistir(IDurum yeniDurum)
    {
        _durum = yeniDurum;
        _durum.SetSiparis(this);
    }

    public void SiparistenVazgec()
    {
        _durum.SiparistenVazgec();
    }

    public void IleriDurumGecis()
    {
        _durum.IleriGecis();
    }

    public string GuncelDurum()
    {
        return _durum.GuncelDurum();
    }

    public bool IptalEdilebilirMi()
    {
        return _durum.IptalEdilebilirMi;
    }
}

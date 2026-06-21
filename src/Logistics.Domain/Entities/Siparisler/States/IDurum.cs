namespace Logistics.Domain.Entities.Siparisler.States;

public interface IDurum
{
    void SiparistenVazgec();
    void IleriGecis();
    string GuncelDurum();
    void SetSiparis(Siparis sp);
    bool IptalEdilebilirMi { get; }
}

using Logistics.Domain.Entities.Urunler;
using Logistics.Domain.Interfaces;

namespace Logistics.Infrastructure.Repositories;

//veri tabanı simülasyonu yapan ve verileri ramde listelerde tutan repo sınıfı.
//IUrunRepository'i gerçekler bu sayede domain sınıfı verilerin nerden geldiğini bilmez.
//domain sınıfı bu sınıfa bağımlı değildir istenirse bir veritabanı rahatlıkla bağlanabilir.
public class UrunRepository : IUrunRepository
{
    private readonly List<IUrun> _urunler;

    public UrunRepository()
    {
        _urunler = new List<IUrun>();
        //başlangıç stokları kurucu metotta belirleniyor çünkü private set
        //diğerleri {} ile set ediliyor çünkü init 
        _urunler.Add(new BasitUrun(10) { Id = 1, Isim = "Kalem Hatali", Sinir = 5, Fiyat = 20, Agirlik = 0.1, HataliMi = true });
        _urunler.Add(new BasitUrun(10) { Id = 2, Isim = "Silgi", Sinir = 10, Fiyat = 10, Agirlik = 0.05 });

        var islemci = new BasitUrun(5) { Id = 3, Isim = "CPU", Sinir = 2, Fiyat = 5000, Agirlik = 0.5 };
        var anakart = new BasitUrun(3) { Id = 4, Isim = "Ram", Sinir = 1, Fiyat = 3000, Agirlik = 1.2 };

        // karmaşık ürün oluşturma ve alt parçalarını ekleme (montaj işlemi)
        // daha derli toplu bir yapı için builder pattern kullanılabilir.
        // stok bilgisi alt parçaların en az sayıda olanına eşittir. Başlangıçta 0 atanır.
        var bilgisayarKasasi = new KarmasikUrun { Id = 5, Isim = "Toplama Bilgisayar", Sinir = 1 };
        bilgisayarKasasi.ParcaEkle(islemci);
        bilgisayarKasasi.ParcaEkle(anakart);

        _urunler.Add(islemci);
        _urunler.Add(anakart);
        _urunler.Add(bilgisayarKasasi);


    }

    public List<IUrun> TumUrunleriGetir()
    {
        return _urunler;
    }

    public IUrun UrunGetir(int id)
    {
        return _urunler.FirstOrDefault(u => ((UrunBase)u).Id == id);
    }

    public void UrunEkle(IUrun urun)
    {
        _urunler.Add(urun);
    }

    public void UrunGuncelle(IUrun urun)
    {
        //veriler veritabanına yazılacağında kullanılabilecek metot
    }
}

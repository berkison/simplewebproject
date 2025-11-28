//ASENKRON HALE GETİRİLECEK 28.11.2025
using Simple.Models;

namespace Simple.Services
{
    public interface IKitapService
    {
        Task<List<Kitaplar>> TumKitaplariGetir();
        Task<Kitaplar> IdIleGetir(int id);

        Task KitapEkle(Kitaplar yeniKitap);

        Task KitapGuncelle(int id, Kitaplar guncelKitap);

        Task<bool> KitapSil(int id);


    }
}

// modelleri almalı gerekli usingler eklenecek...

using Simple.Models;

namespace Simple.Services
{
    public interface IKitapService
    {
        List<Kitaplar> TumKitaplariGetir();
        Kitaplar IdIleGetir(int id);

        void KitapEkle(Kitaplar yeniKitap);

        void KitapGuncelle(int id, Kitaplar guncelKitap);

        bool KitapSil(int id);


    }
}

using Simple.Data;
using Simple.Models;

namespace Simple.Services
{
    public class KitapService:IKitapService // IKitapService'ten miras aldık
    {
        private readonly SimpleContext _context;
        public KitapService(SimpleContext context)

        {

            _context = context;
        }

        public List<Kitaplar> TumKitaplariGetir()
        {
            return _context.Kitaplar.ToList();
        }

        public Kitaplar IdIleGetir(int id)
        {
            return _context.Kitaplar.Find(id);
        }
        public void KitapEkle(Kitaplar yeniKitap)
        {
            _context.Kitaplar.Add(yeniKitap);
            _context.SaveChanges();
        }

        public void KitapGuncelle(int id, Kitaplar guncelKitap)
        {
            var kitap = _context.Kitaplar.Find(id);
            if (kitap != null)
            {
                kitap.Ad = guncelKitap.Ad;
                kitap.Yazar = guncelKitap.Yazar;
                kitap.SayfaSayisi = guncelKitap.SayfaSayisi;
                _context.SaveChanges();
            }
        }


        public bool KitapSil(int id)
        {
            var kitap = _context.Kitaplar.Find(id);
            if(kitap != null)
            {
                _context.Kitaplar.Remove(kitap);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

using Simple.Data;
using Simple.Models;
using Microsoft.EntityFrameworkCore;

namespace Simple.Services
{
    public class KitapService : IKitapService // IKitapService'ten miras aldık
    {
        private readonly SimpleContext _context;
        public KitapService(SimpleContext context)

        {
            _context = context;
        }

        public async Task<List<Kitaplar>> TumKitaplariGetir()
        {
            return await _context.Kitaplar.ToListAsync();
        }

        public async Task<Kitaplar> IdIleGetir(int id)
        {
            return await _context.Kitaplar.FindAsync(id);
        }
        public async Task KitapEkle(Kitaplar yeniKitap)
        {
            await _context.Kitaplar.AddAsync(yeniKitap);
            await _context.SaveChangesAsync();
        }

        public async Task KitapGuncelle(int id, Kitaplar guncelKitap)
        {
            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap != null)
            {
                kitap.Ad = guncelKitap.Ad;
                kitap.Yazar = guncelKitap.Yazar;
                kitap.SayfaSayisi = guncelKitap.SayfaSayisi;
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> KitapSil(int id)
        {
            var kitap = await _context.Kitaplar.FindAsync(id);
            if(kitap != null)
            {
                 _context.Kitaplar.Remove(kitap);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

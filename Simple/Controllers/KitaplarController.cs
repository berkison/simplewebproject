using Microsoft.AspNetCore.Mvc;
using Simple.Data;
using Simple.Models;

namespace Simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitaplarController : ControllerBase
    {
        private readonly SimpleContext _context;

        public KitaplarController(SimpleContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult KitapEkle(Kitaplar yeniKitap)
        {
            _context.kitaplar.Add(yeniKitap);
            _context.SaveChanges();
            return Ok("Kitap başarıyla kaydedildi!");
        }

        [HttpGet]
        public IActionResult KitaplariGetir() {

            var kitaplar = _context.kitaplar.ToList();

            return Ok(kitaplar);
        }

        [HttpDelete("{id}")]

        public IActionResult KitaplariDelete(int id)
        {
            var silinecekKitap = _context.kitaplar.Find(id);

            if(silinecekKitap == null)
            {
                return NotFound("Böyle Bir Kitap Zaten YOK");
            }
            _context.kitaplar.Remove(silinecekKitap);
            _context.SaveChanges();
            return Ok("KİTAP SİLİNMESİ DÖNDÜ");

        }
        [HttpPut("{id}")]

        public IActionResult KitaplariGuncelle(int id, Kitaplar guncelKitap)
        {
            var guncellenecekKitap = _context.kitaplar.Find(id);
            if(guncellenecekKitap == null)
            {
                return NotFound("Güncellemek istediğiiz kitap bulunamadı");

            }
            guncellenecekKitap.Ad = guncelKitap.Ad;
            guncellenecekKitap.SayfaSayisi = guncelKitap.SayfaSayisi;
            guncellenecekKitap.Yazar = guncelKitap.Yazar;
            _context.SaveChanges();

            return Ok("Başarıyla Güncellendi");
        }

        
        
    }
}
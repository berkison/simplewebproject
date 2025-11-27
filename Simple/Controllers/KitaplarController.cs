using Microsoft.AspNetCore.Mvc;
using Simple.Models;
using Simple.Services; // Service klasörünü ekle

namespace Simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitaplarController : ControllerBase
    {
        // ARTIK CONTEXT YOK, SERVICE VAR
        private readonly IKitapService _service;

        public KitaplarController(IKitapService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult KitaplariGetir()
        {
            var kitaplar = _service.TumKitaplariGetir();
            return Ok(kitaplar);
        }

        [HttpPost]
        public IActionResult KitapEkle(Kitaplar yeniKitap)
        {
            _service.KitapEkle(yeniKitap);
            return Ok("Kitap başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult KitapGuncelle(int id, Kitaplar guncelKitap)
        {
            // Kontrolü servise bırakabilirsin veya burada yapabilirsin
            var kitap = _service.IdIleGetir(id);
            if (kitap == null) return NotFound("Kitap bulunamadı");

            _service.KitapGuncelle(id, guncelKitap);
            return Ok("Kitap güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult KitapSil(int id)
        {
            var sonuc = _service.KitapSil(id);
            if (!sonuc) return NotFound("Silinecek kitap yok.");

            return Ok("Kitap silindi.");
        }
    }
}
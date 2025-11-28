using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Simple.DTOs;
using Simple.Models;
using Simple.Services;

namespace Simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitaplarController : ControllerBase
    {
        private readonly IKitapService _service;
        private readonly IMapper _mapper;

        // Tek ve doğru yapıcı metot (Constructor)
        public KitaplarController(IKitapService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // --- LİSTELEME (GET) ---
        [HttpGet]
        public IActionResult KitaplariGetir()
        {
            // Servisten gelen verinin tipi: List<Kitaplar>
            var gercekKitaplar = _service.TumKitaplariGetir();

            // AutoMapper: List<Kitaplar> -> List<KitapListelemeDto>
            var dtoListe = _mapper.Map<List<KitapListelemeDTO>>(gercekKitaplar);

            return Ok(dtoListe);
        }

        // --- EKLEME (POST) ---
        [HttpPost]
        public IActionResult KitapEkle(KitapEklemeDTO gelenVeri)
        {
            // AutoMapper: KitapEklemeDto -> Kitaplar
            var yeniKitap = _mapper.Map<Kitaplar>(gelenVeri);

            _service.KitapEkle(yeniKitap);

            return Ok("Kitap başarıyla eklendi.");
        }

        // --- GÜNCELLEME (PUT) ---
        [HttpPut("{id}")]
        public IActionResult KitapGuncelle(int id, KitapEklemeDTO guncellenecekVeri)
        {
            var mevcutKitap = _service.IdIleGetir(id);
            if (mevcutKitap == null) return NotFound("Kitap bulunamadı!");

            // AutoMapper: DTO verilerini mevcut 'Kitaplar' nesnesinin üzerine yazar
            _mapper.Map(guncellenecekVeri, mevcutKitap);

            _service.KitapGuncelle(id, mevcutKitap);
            return Ok("Kitap güncellendi.");
        }

        // --- SİLME (DELETE) ---
        [HttpDelete("{id}")]
        public IActionResult KitapSil(int id)
        {
            var sonuc = _service.KitapSil(id);
            if (!sonuc) return NotFound("Silinecek kitap yok.");

            return Ok("Kitap silindi.");
        }
    }
}
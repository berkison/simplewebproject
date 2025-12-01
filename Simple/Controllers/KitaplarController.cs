using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> KitaplariGetir() // Düzeltme: Task<IActionResult>
        {

            //throw new Exception("DENEME");

            // Servisten gelen verinin tipi: List<Kitaplar>
            var gercekKitaplar = await _service.TumKitaplariGetir();

            // AutoMapper: List<Kitaplar> -> List<KitapListelemeDto>
            var dtoListe = _mapper.Map<List<KitapListelemeDTO>>(gercekKitaplar);

            return Ok(dtoListe);
        }

        // --- EKLEME (POST) ---
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> KitapEkle(KitapEklemeDTO gelenVeri) // Düzeltme: async Task
        {
            // AutoMapper: KitapEklemeDto -> Kitaplar
            var yeniKitap = _mapper.Map<Kitaplar>(gelenVeri);

            await _service.KitapEkle(yeniKitap); // Düzeltme: await eklendi

            return Ok("Kitap başarıyla eklendi.");
        }

        // --- GÜNCELLEME (PUT) ---
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> KitapGuncelle(int id, KitapEklemeDTO guncellenecekVeri) // Düzeltme: async Task
        {
            var mevcutKitap = await _service.IdIleGetir(id); // Düzeltme: await eklendi
            if (mevcutKitap == null) return NotFound("Kitap bulunamadı!");

            // AutoMapper: DTO verilerini mevcut 'Kitaplar' nesnesinin üzerine yazar
            _mapper.Map(guncellenecekVeri, mevcutKitap);

            await _service.KitapGuncelle(id, mevcutKitap); // Düzeltme: await eklendi
            return Ok("Kitap güncellendi.");
        }

        // --- SİLME (DELETE) ---
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> KitapSil(int id) // Düzeltme: async Task
        {
            var sonuc = await _service.KitapSil(id); // Düzeltme: await eklendi
            if (!sonuc) return NotFound("Silinecek kitap yok.");

            return Ok("Kitap silindi.");
        }
    }
}
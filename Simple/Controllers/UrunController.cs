using Microsoft.AspNetCore.Mvc;
using Simple.Data;
using Simple.Models;

namespace Simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunController : ControllerBase
    {
        private readonly SimpleContext _context;

        public UrunController(SimpleContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult UrunEkle(Urun yeniUrun)
        {
            _context.Urunler.Add(yeniUrun);
            _context.SaveChanges();
            return Ok("Kitap başarıyla kaydedildi!");
        }
    }
}
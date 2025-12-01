using Microsoft.AspNetCore.Mvc;
using Simple.Services;

namespace Simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiyatController : ControllerBase
    {
        private readonly FiyatTakipService _service;

        public FiyatController(FiyatTakipService service)
        {
            _service = service;
        }

        [HttpPost("sorgula")]
        public async Task<IActionResult> FiyatSorgula([FromBody] UrlIstegi istek)
        {
            try
            {
                var sonuc = await _service.FiyatCek(istek.Link);
                return Ok(sonuc);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Hata = ex.Message });
            }
        }
    }

    public class UrlIstegi
    {
        public string Link { get; set; }
    }
}
using HtmlAgilityPack;

namespace Simple.Services
{
    public class FiyatTakipService
    {
        private readonly HttpClient _httpClient;

        public FiyatTakipService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Siteye "Ben Chrome tarayıcısıyım" diyoruz (Bot korumasını aşmak için)
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        }

        public async Task<UrunBilgisiDto> FiyatCek(string urunUrl)
        {
            // 1. Sayfanın HTML'ini indir
            var html = await _httpClient.GetStringAsync(urunUrl);

            // 2. HtmlAgilityPack ile yükle
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // 3. Verileri Çek (XPATH Yöntemi)
            // NOT: Bu XPath'ler siteden siteye değişir! (Aşağıda nasıl bulacağını anlatacağım)

            // Örnek: Bir kitap sitesi için varsayılan yollar (Bunları değiştireceğiz)
            //var adNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"envoy\"]/div/h1");
            var fiyatNode = doc.DocumentNode.SelectSingleNode("//p[@class='old-price']");

            // Eğer bulamazsa hata dönmeyelim, null dönelim


            return new UrunBilgisiDto
            {
                //UrunAdi = adNode.InnerText.Trim(),
                Fiyat = fiyatNode.InnerText.Trim(),
                Url = urunUrl
            };
        }
    }

    // DTO'yu da buraya sıkıştıralım (Normalde DTOs klasöründe olmalı)
    public class UrunBilgisiDto
    {
        public string UrunAdi { get; set; }
        public string Fiyat { get; set; }
        public string Url { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.WhoWeAreDtos;

namespace RealEstate_Dapper_UI.ViewComponents.HomePage
{
    public class _DefaultWhoWeAreComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultWhoWeAreComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => sslPolicyErrors == System.Net.Security.SslPolicyErrors.None || cert.Issuer.Equals("CN=localhost")
            };

            using var client = new HttpClient(handler);
            var responseMessage = await client.GetAsync("https://localhost:44305/api/WhoWeAreDetail");

            if (responseMessage.IsSuccessStatusCode) 
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultWhoWeAreDetailDto>>(jsonData);

                ViewBag.title = values.Select(x => x.Title).FirstOrDefault();
                ViewBag.subtitle = values.Select(x => x.Subtitle).FirstOrDefault();
                ViewBag.description1 = values.Select(x => x.Description1).FirstOrDefault();
                ViewBag.description2 = values.Select(x => x.Description2).FirstOrDefault();

                return View();
            }

            return View();

        }
    }
}

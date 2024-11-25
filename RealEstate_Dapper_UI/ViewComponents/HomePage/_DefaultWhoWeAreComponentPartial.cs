using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.ServiceDtos;
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
            using var client2 = new HttpClient(handler);

            var responseMessage = await client.GetAsync("https://localhost:44305/api/WhoWeAreDetail");
            var responseMessage2 = await client.GetAsync("https://localhost:44305/api/Services");

            if (responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode) 
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultWhoWeAreDetailDto>>(jsonData);
                var values2 = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData2);

                ViewBag.title = values.Select(x => x.Title).FirstOrDefault();
                ViewBag.subtitle = values.Select(x => x.Subtitle).FirstOrDefault();
                ViewBag.description1 = values.Select(x => x.Description1).FirstOrDefault();
                ViewBag.description2 = values.Select(x => x.Description2).FirstOrDefault();

                return View(values2);
            }

            return View();

        }
    }
}

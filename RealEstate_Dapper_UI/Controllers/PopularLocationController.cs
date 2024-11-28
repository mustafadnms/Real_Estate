using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.PopularLocationDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
	public class PopularLocationController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public PopularLocationController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => sslPolicyErrors == System.Net.Security.SslPolicyErrors.None || cert.Issuer.Equals("CN=localhost")
			};

			using var client = new HttpClient(handler);
			var responseMessage = await client.GetAsync("https://localhost:44305/api/PopularLocations");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultPopularLocationDto>>(jsonData);

				return View(values);
			}
			return View();
		}

		[HttpGet]
		public IActionResult CreatePopularLocation()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreatePopularLocation(CreatePopularLocaitonDto createPopularLocationDto)
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			};

			var client = new HttpClient(handler);
			var jsonData = JsonConvert.SerializeObject(createPopularLocationDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:44305/api/PopularLocations", stringContent);

			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return View();
		}

		public async Task<IActionResult> DeletePopularLocation(int id)
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			};

			var client = new HttpClient(handler);
			var responseMessage = await client.DeleteAsync($"https://localhost:44305/api/PopularLocations/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		[HttpGet]
		public async Task<IActionResult> UpdatePopularLocation(int id)
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			};

			var client = new HttpClient(handler);
			var responseMessage = await client.GetAsync($"https://localhost:44305/api/PopularLocations/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdatePopularLocaitonDto>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdatePopularLocation(UpdatePopularLocaitonDto updatePopularLocationDto)
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			};

			var client = new HttpClient(handler);
			var jsonData = JsonConvert.SerializeObject(updatePopularLocationDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("https://localhost:44305/api/PopularLocations/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


	}
}

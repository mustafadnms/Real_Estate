using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.BottomGridDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
	public class BottomGridController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public BottomGridController(IHttpClientFactory httpClientFactory)
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
			var responseMessage = await client.GetAsync("https://localhost:44305/api/BottomGrids");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultBottomGridDto>>(jsonData);

				return View(values);
			}
			return View();
		}

		[HttpGet]
		public IActionResult CreateBottomGrid()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateBottomGrid(CreateBottomGridDto createBottomGridDto)
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			};

			var client = new HttpClient(handler);
			var jsonData = JsonConvert.SerializeObject(createBottomGridDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:44305/api/BottomGrids", stringContent);

			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return View();
		}

		public async Task<IActionResult> DeleteBottomGrid(int id)
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			};

			var client = new HttpClient(handler);
			var responseMessage = await client.DeleteAsync($"https://localhost:44305/api/BottomGrids/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}


		[HttpGet]
		public async Task<IActionResult> UpdateBottomGrid(int id)
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			};

			var client = new HttpClient(handler);
			var responseMessage = await client.GetAsync($"https://localhost:44305/api/BottomGrids/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateBottomGridDto>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateBottomGrid(UpdateBottomGridDto updateBottomGridDto)
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			};

			var client = new HttpClient(handler);
			var jsonData = JsonConvert.SerializeObject(updateBottomGridDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("https://localhost:44305/api/BottomGrids/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}

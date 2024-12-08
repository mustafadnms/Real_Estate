using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.CategoryDtos;
using RealEstate_Dapper_UI.Dtos.ProductDtos;
using RealEstate_Dapper_UI.Services;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.Controllers
{
	[Area("EstateAgent")]
	public class MyAdvertsController : Controller
	{

		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILoginService _loginService;

        public MyAdvertsController(IHttpClientFactory httpClientFactory, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }

        public async Task<IActionResult> ActiveAdverts()
		{
			var userId = _loginService.GetUserId;

			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => sslPolicyErrors == System.Net.Security.SslPolicyErrors.None || cert.Issuer.Equals("CN=localhost")
			};

			using var client = new HttpClient(handler);
			var responseMessage = await client.GetAsync("https://localhost:44305/api/Products/ProductAdvertsListByEmployeeTrue?id=" + userId);

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductAdvertListWithCategoryByEmployeeDto>>(jsonData);

				return View(values);
			}
			return View();
		}

		public async Task<IActionResult> PassiveAdverts()
		{
			var userId = _loginService.GetUserId;

			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => sslPolicyErrors == System.Net.Security.SslPolicyErrors.None || cert.Issuer.Equals("CN=localhost")
			};

			using var client = new HttpClient(handler);
			var responseMessage = await client.GetAsync("https://localhost:44305/api/Products/ProductAdvertListByEmployeeByFalse?id=" + userId);

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductAdvertListWithCategoryByEmployeeDto>>(jsonData);

				return View(values);
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> CreateAdvert()
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => sslPolicyErrors == System.Net.Security.SslPolicyErrors.None || cert.Issuer.Equals("CN=localhost")
			};

			using var client = new HttpClient(handler);
			var responseMessage = await client.GetAsync("https://localhost:44305/api/Categories");

			var jsonData = await responseMessage.Content.ReadAsStringAsync();
			var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

			List<SelectListItem> categoryValues = (from x in values.ToList()
												   select new SelectListItem
												   {
													   Text = x.CategoryName,
													   Value = x.CategoryId.ToString()
												   }).ToList();
			ViewBag.v = categoryValues;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAdvert(CreateProductDto createProductDto)
		{
			createProductDto.DealOfTheDay = false;
			createProductDto.AdvertisementDate = DateTime.Now;
			createProductDto.ProductStatus = true;

			var userId = _loginService.GetUserId;
			createProductDto.EmployeeId = int.Parse(userId);

			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
			};

			var client = new HttpClient(handler);
			var jsonData = JsonConvert.SerializeObject(createProductDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:44305/api/Products", stringContent);

			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return View();
		}

	}
}

﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.ProductDtos;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.Controllers
{
	[Area("EstateAgent")]
	public class MyAdvertsController : Controller
	{

		private readonly IHttpClientFactory _httpClientFactory;

		public MyAdvertsController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index(int id)
		{
			id = 1;
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => sslPolicyErrors == System.Net.Security.SslPolicyErrors.None || cert.Issuer.Equals("CN=localhost")
			};

			using var client = new HttpClient(handler);
			var responseMessage = await client.GetAsync("https://localhost:44305/api/Products/ProductAdvertsListByEmployee?id=" + id);

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductAdvertListWithCategoryByEmployeeDto>>(jsonData);

				return View(values);
			}
			return View();
		}
	}
}
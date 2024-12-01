using Microsoft.AspNetCore.SignalR;

namespace RealEstate_Dapper_Api.Hubs
{
	public class SignalRHub:Hub
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public SignalRHub(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task SendCategoryCount()
		{
			var handler7 = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => sslPolicyErrors == System.Net.Security.SslPolicyErrors.None || cert.Issuer.Equals("CN=localhost")
			};

			using var client7 = new HttpClient(handler7);
			var responseMessage7 = await client7.GetAsync("https://localhost:44305/api/Statistics/CategoryCount");
			var value = await responseMessage7.Content.ReadAsStringAsync();

			await Clients.All.SendAsync("ReceiveCategoryCount", value);
		}


	}
}

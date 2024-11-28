using Dapper;
using RealEstate_Dapper_Api.Dtos.PopularLocationDtos;
using RealEstate_Dapper_Api.Dtos.ServiceDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.PopularLocaitonRepositories
{
    public class PopularLocationRepository : IPopularLocationRepository
    {
        private readonly Context _context;

        public PopularLocationRepository(Context context)
        {
            _context = context;
        }

		public async void CreatePopularLocation(CreatePopularLocationDto createPopularLocationDto)
		{
			string query = "insert into PopularLocation (CityName, ImageUrl) values (@cityName, @imageUrl)";

			var parameters = new DynamicParameters();
			parameters.Add("@cityName", createPopularLocationDto.CityName);
			parameters.Add("@imageUrl", createPopularLocationDto.ImageUrl);


			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

		public async void DeletePopularLocation(int id)
		{
			string query = "Delete From PopularLocation Where LocationId = @popularLocationId";
			var parameters = new DynamicParameters();
			parameters.Add("@popularLocationId", id);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

		public async Task<List<ResultPopularLocationDto>> GetAllPopularLocaitonAsync()
        {
            string query = "Select * From PopularLocation";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultPopularLocationDto>(query);
                return values.ToList();
            }
        }

		public async Task<GetByIdPopularLocationDto> GetPopularLocation(int id)
		{
			string query = "Select * From PopularLocation Where LocationId=@popularLocationId";

			var parameters = new DynamicParameters();
			parameters.Add("@popularLocationId", id);

			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<GetByIdPopularLocationDto>(query, parameters);

				return values;
			}
		}

		public async void UpdatePopularLocation(UpdatePopularLocationDto updatePopularLocationDto)
		{
			string query = "Update PopularLocation Set CityName=@cityName, ImageUrl=@imageUrl Where LocationId=@popularLocationId";

			var parameters = new DynamicParameters();
			parameters.Add("@cityName", updatePopularLocationDto.CityName);
			parameters.Add("@imageUrl", updatePopularLocationDto.ImageUrl);
			parameters.Add("@popularLocationId", updatePopularLocationDto.LocationId);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}
	}
}

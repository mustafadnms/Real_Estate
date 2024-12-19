﻿using Dapper;
using RealEstate_Dapper_Api.Dtos.ServiceDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ServiceRepository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly Context _context;

        public ServiceRepository(Context context)
        {
            _context = context;
        }
        public async Task CreateService(CreateServiceDto createServiceDto)
        {
			string query = "insert into Service (ServiceName, ServiceStatus) values (@serviceName, @serviceStatus)";

			var parameters = new DynamicParameters();
			parameters.Add("@serviceName", createServiceDto.ServiceName);
			parameters.Add("@serviceStatus", true);


			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

        public async Task DeleteService(int id)
        {
			string query = "Delete From Service Where ServiceId = @serviceId";
			var parameters = new DynamicParameters();
			parameters.Add("@serviceId", id);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

        public async Task<List<ResultServiceDto>> GetAllService()
        {
            string query = "Select * From Service";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultServiceDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdServiceDto> GetService(int id)
        {
			string query = "Select * From Service Where ServiceId=@serviceId";

			var parameters = new DynamicParameters();
			parameters.Add("@serviceId", id);

			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<GetByIdServiceDto>(query, parameters);

				return values;
			}
		}

        public async Task UpdateService(UpdateServiceDto updateServiceDto)
        {
			string query = "Update Service Set ServiceName=@serviceName, ServiceStatus=@serviceStatus Where ServiceId=@serviceId";

			var parameters = new DynamicParameters();
			parameters.Add("@serviceName", updateServiceDto.ServiceName);
			parameters.Add("@serviceStatus", updateServiceDto.ServiceStatus);
			parameters.Add("@serviceId", updateServiceDto.ServiceId);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}
    }
}

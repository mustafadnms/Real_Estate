﻿using Dapper;
using RealEstate_Dapper_Api.Dtos.WhoWeAreDetailDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.WhoWeAreRepository
{
    public class WhoWeAreDetailRepository : IWhoWeAreDetailRepository
    {
        private readonly Context _context;

        public WhoWeAreDetailRepository(Context context)
        {
            _context = context;
        }
        public async Task CreateWhoWeAreDetail(CreateWhoWeAreDetailDto createWhoWeAreDetailDto)
        {
            string query = "insert into WhoWeAreDetail (Title, Subtitle, Description1, Description2) values (@title, @subtitle, @description1, @description2)";

            var parameters = new DynamicParameters();
            parameters.Add("@title", createWhoWeAreDetailDto.Title);
            parameters.Add("@subtitle", createWhoWeAreDetailDto.Subtitle);
            parameters.Add("@description1", createWhoWeAreDetailDto.Description1);
            parameters.Add("@description2", createWhoWeAreDetailDto.Description2);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteWhoWeAreDetail(int id)
        {
            string query = "Delete From WhoWeAreDetail Where WhoWeAreDetailId = @whowearedetailId";
            var parameters = new DynamicParameters();
            parameters.Add("whowearedetailId", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultWhoWeAreDetailDto>> GetAllWhoWeAreDetail()
        {
            string query = "Select * From WhoWeAreDetail";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultWhoWeAreDetailDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdWhoWeAreDetailDto> GetWhoWeAreDetail(int id)
        {
            string query = "Select * From WhoWeAreDetail Where WhoWeAreDetailId=@whoWeAreDetailId";

            var parameters = new DynamicParameters();
            parameters.Add("@whoWeAreDetailId", id);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIdWhoWeAreDetailDto>(query, parameters);

                return values;
            }
        }

        public async Task UpdateWhoWeAreDetail(UpdateWhoWeAreDetailDto updateWhoWeAreDetailDto)
        {
            string query = "Update WhoWeAreDetail Set Title=@title, Subtitle=@subtitle, Description1=@description1, Description2=@description2 Where WhoWeAreDetailId=@whoWeAreDetailId";

            var parameters = new DynamicParameters();
            parameters.Add("@title", updateWhoWeAreDetailDto.Title);
            parameters.Add("@subtitle", updateWhoWeAreDetailDto.Subtitle);
            parameters.Add("@description1", updateWhoWeAreDetailDto.Description1);
            parameters.Add("@description2", updateWhoWeAreDetailDto.Description2);
            parameters.Add("@whoWeAreDetailId", updateWhoWeAreDetailDto.WhoWeAreDetailId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}

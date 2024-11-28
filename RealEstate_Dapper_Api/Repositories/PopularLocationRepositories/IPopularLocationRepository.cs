using RealEstate_Dapper_Api.Dtos.PopularLocationDtos;
using RealEstate_Dapper_Api.Dtos.PopularLocationDtos;

namespace RealEstate_Dapper_Api.Repositories.PopularLocaitonRepositories
{
    public interface IPopularLocationRepository
    {
        Task<List<ResultPopularLocationDto>> GetAllPopularLocaitonAsync();
		void CreatePopularLocation(CreatePopularLocationDto createPopularLocationDto);
		void DeletePopularLocation(int id);
		void UpdatePopularLocation(UpdatePopularLocationDto updatePopularLocationDto);
		Task<GetByIdPopularLocationDto> GetPopularLocation(int id);
	}
}

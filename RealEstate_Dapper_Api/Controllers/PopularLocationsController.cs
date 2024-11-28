using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.PopularLocationDtos;
using RealEstate_Dapper_Api.Repositories.PopularLocaitonRepositories;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopularLocationsController : ControllerBase
    {
        private readonly IPopularLocationRepository _popularLocaitonRepository;

        public PopularLocationsController(IPopularLocationRepository popularLocaitonRepository)
        {
            _popularLocaitonRepository = popularLocaitonRepository;
        }

        [HttpGet]
        public async Task<IActionResult> PopularLocationList()
        {
            var value = await _popularLocaitonRepository.GetAllPopularLocaitonAsync();
            return Ok(value);
        }


		[HttpPost]
		public async Task<IActionResult> CreatePopularLocation(CreatePopularLocationDto createPopularLocationDto)
		{
			_popularLocaitonRepository.CreatePopularLocation(createPopularLocationDto);
			return Ok("Lokasyon Başarılı Bir Şekilde Eklendi");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePopularLocation(int id)
		{
			_popularLocaitonRepository.DeletePopularLocation(id);
			return Ok("Lokasyon Başarılı Bir Şekilde Silindi");
		}

		[HttpPut]
		public async Task<IActionResult> UpdatePopularLocation(UpdatePopularLocationDto updatePopularLocationDto)
		{
			_popularLocaitonRepository.UpdatePopularLocation(updatePopularLocationDto);
			return Ok("Lokasyon Başarıyla Güncellendi");
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPopularLocation(int id)
		{
			var value = await _popularLocaitonRepository.GetPopularLocation(id);
			return Ok(value);
		}

	}
}

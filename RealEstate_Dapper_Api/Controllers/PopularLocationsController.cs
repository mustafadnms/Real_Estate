using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}

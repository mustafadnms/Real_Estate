using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.WhoWeAreDetailDtos;
using RealEstate_Dapper_Api.Repositories.CategoryRepository;
using RealEstate_Dapper_Api.Repositories.WhoWeAreRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhoWeAreDetailController : ControllerBase
    {

        private readonly IWhoWeAreDetailRepository _whoWeAreRepository;

        public WhoWeAreDetailController(IWhoWeAreDetailRepository whoWeAreDetailRepository)
        {
            _whoWeAreRepository = whoWeAreDetailRepository;
        }

        [HttpGet]
        public async Task<IActionResult> WhoWeAreDetailList()
        {
            var value = await _whoWeAreRepository.GetAllWhoWeAreDetailAsync();
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWhoWeAreDetail(CreateWhoWeAreDetailDto createWhoWeAreDetailDto)
        {
            _whoWeAreRepository.CreateWhoWeAreDetail(createWhoWeAreDetailDto);
            return Ok("Hakkımızda Kısmı Başarılı Bir Şekilde Eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWhoWeAreDetail(int id)
        {
            _whoWeAreRepository.DeleteWhoWeAreDetail(id);
            return Ok("Hakkımızda Kısmı Başarılı Bir Şekilde Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWhoWeAreDetail(UpdateWhoWeAreDetailDto updateWhoWeAreDetailDto)
        {
            _whoWeAreRepository.UpdateWhoWeAreDetail(updateWhoWeAreDetailDto);
            return Ok("Hakkımızda Kısmı Başarıyla Güncellendi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWhoWeAreDetail(int id)
        {
            var value = await _whoWeAreRepository.GetWhoWeAreDetail(id);
            return Ok(value);
        }

    }
}

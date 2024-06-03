using Application.Interface.Service;
using Domain.Model.Size;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/sizes")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizesController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSizes()
        {
            var result = await _sizeService.GetSizes();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSizeById(int id)
        {
            var result = await _sizeService.GetSizeById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSize([FromForm] SizePostModel sizeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _sizeService.CreateSize(sizeModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSize([FromForm] SizePutModel sizeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _sizeService.UpdateSize(sizeModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSize(int id)
        {
            var result = await _sizeService.DeleteSize(id);
            return Ok(result);
        }
    }
}

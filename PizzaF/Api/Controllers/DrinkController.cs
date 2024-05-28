using Application.Interface.Service;
using Domain.Model.Drink;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/drinks")]
    [ApiController]
    public class DrinkController : ControllerBase
    {
        private readonly IDrinkService _drinkService;

        public DrinkController(IDrinkService drinkService)
        {
            _drinkService = drinkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListDrinkAsync([FromQuery] DrinkSearchModel searchModel)
        {
            var result = await _drinkService.GetListDrinkAsync(searchModel);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrinkByIdAsync(int id)
        {
            var result = await _drinkService.GetDrinkByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDrinkAsync([FromForm] DrinkPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _drinkService.CreateDrinkAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDrinkAsync([FromForm] DrinkPushModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _drinkService.UpdateDrinkAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

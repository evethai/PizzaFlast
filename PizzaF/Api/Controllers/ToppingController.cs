using Application.Interface.Service;
using Domain.Model.Topping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToppingController : ControllerBase
    {
        private readonly IToppingService _toppingService;

        public ToppingController(IToppingService toppingService)
        {
            _toppingService = toppingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetToppings()
        {
            var result = await _toppingService.GetToppings();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetToppingById(int id)
        {
            var result = await _toppingService.GetToppingById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopping([FromForm] ToppingPostModel toppingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _toppingService.CreateTopping(toppingModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTopping([FromForm] ToppingPutModel toppingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _toppingService.UpdateTopping(toppingModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopping(int id)
        {
            var result = await _toppingService.DeleteTopping(id);
            return Ok(result);
        }
    }
}

using Application.Interface.Service;
using Domain.Model.Pizza;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;

        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListPizzaAsync([FromQuery] PizzaSearchModel searchModel)
        {
            var result = await _pizzaService.GetListPizzaAsync(searchModel);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPizzaByIdAsync(int id)
        {
            var result = await _pizzaService.GetPizzaByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePizzaAsync([FromForm] PizzaPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _pizzaService.CreatePizzaAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePizzaAsync([FromForm] PizzaPutModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _pizzaService.UpdatePizzaAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

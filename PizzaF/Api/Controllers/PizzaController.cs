using Application.Interface.Service;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
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
    }
}

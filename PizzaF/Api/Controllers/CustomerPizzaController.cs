using Application.Interface.Service;
using Domain.Model.CustomerPizza;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/cus-pizzas")]
    [ApiController]
    public class CustomerPizzaController : ControllerBase
    {
        private readonly ICustomerPizzaService _customerPizzaService;

        public CustomerPizzaController(ICustomerPizzaService customerPizzaService)
        {
            _customerPizzaService = customerPizzaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerPizzaById(int id)
        {
            var result = await _customerPizzaService.GetCustomerPizzaById(id);
            return Ok(result);
        }

        [HttpGet("order/{id}")]
        public async Task<IActionResult> GetCustomerPizzasByOrderId(int id)
        {
            var result = await _customerPizzaService.GetCustomerPizzasByOrderId(id);
            return Ok(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateCustomerPizza([FromForm] CustomerPizzaPostModel customerPizzaModel)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return base.BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        var result = await _customerPizzaService.CreateCustomerPizza(customerPizzaModel);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut]
        public async Task<IActionResult> UpdateCustomerPizza([FromForm] CustomerPizzaPutModel customerPizzaModel)
        {
            if (!ModelState.IsValid)
            {
                return base.BadRequest(ModelState);
            }
            try
            {
                var result = await _customerPizzaService.UpdateCustomerPizza(customerPizzaModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

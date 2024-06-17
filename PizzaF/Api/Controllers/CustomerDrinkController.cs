using Application.Interface.Service;
using Domain.Model.CustomerDrink;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/cus-drinks")]
    [ApiController]
    public class CustomerDrinkController : ControllerBase
    {
        private readonly ICustomerDrinkService _customerDrinkService;

        public CustomerDrinkController(ICustomerDrinkService customerDrinkService)
        {
            _customerDrinkService = customerDrinkService;
        }

        [HttpGet("order/{id}")]
        public async Task<IActionResult> GetCustomerDrinkByOrderId(int id)
        {
            var result = await _customerDrinkService.GetCustomerDrinkByOrderId(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerDrinkById(int id)
        {
            var result = await _customerDrinkService.GetCustomerDrinkById(id);
            return Ok(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateCustomerDrink([FromForm] CustomerDrinkPostModel customerDrinkModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        var result = await _customerDrinkService.CreateCustomerDrink(customerDrinkModel);
        //        return Ok(result);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut]
        public async Task<IActionResult> UpdateCustomerDrink([FromForm] CustomerDrinkPutModel customerDrinkModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _customerDrinkService.UpdateCustomerDrink(customerDrinkModel);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

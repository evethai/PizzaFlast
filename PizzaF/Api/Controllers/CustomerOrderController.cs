using Application.Interface.Service;
using Domain.Enum;
using Domain.Model.CustomerOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/cus-orders")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;
        public CustomerOrderController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerOrder([FromBody] CustomerOrderPostModel customerOrderModel)
        {
            try
            {
                var result = await _customerOrderService.CreateOrder(customerOrderModel);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerOrderById(int id)
        {
            try
            {
                var result = await _customerOrderService.GetBillByOrderId(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerOrder(int id, [FromForm] OrderStatus status)
        {
            try
            {
                var result = await _customerOrderService.UpdateOrder(id, status);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

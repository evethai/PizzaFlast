using Domain.Model.Payment;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
namespace Api.Controllers
{
    [ApiController]
    [Route("api/Payment")]
    public class PaymentController : ControllerBase
    {



        private static readonly HttpClient client = new HttpClient();
        private const string AccessKey = "F8BBA842ECF85";
        private const string SecretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";

        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] PaymentModel requestModel)
        {
            var request = new CollectionLinkRequest
            {
                orderInfo = "pay with MoMo",
                partnerCode = "MOMO",
                redirectUrl = "https://www.linkedin.com/in/danggkhoaaaa/",
                ipnUrl = "https://localhost:7123/api/Payment/ipn",
                amount = (long)requestModel.Amount,
                orderId = requestModel.OrderId.ToString(),
                requestId = Guid.NewGuid().ToString(),
                requestType = "payWithMethod",
                extraData = "",
                partnerName = "ToTe Payment",
                storeId = "test",
                orderGroupId = "",
                autoCapture = true,
                lang = "vi"
            };

            var rawSignature = $"accessKey={AccessKey}&amount={request.amount}&extraData={request.extraData}&ipnUrl={request.ipnUrl}&orderId={request.orderId}&orderInfo={request.orderInfo}&partnerCode={request.partnerCode}&redirectUrl={request.redirectUrl}&requestId={request.requestId}&requestType={request.requestType}";
            request.signature = GetSignature(rawSignature, SecretKey);

            var httpContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var quickPayResponse = await client.PostAsync("https://test-payment.momo.vn/v2/gateway/api/create", httpContent);

            if (!quickPayResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)quickPayResponse.StatusCode, await quickPayResponse.Content.ReadAsStringAsync());
            }

            var responseContent = await quickPayResponse.Content.ReadAsStringAsync();
            return Ok(responseContent);
        }

        private static string GetSignature(string text, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.ASCII.GetBytes(key)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.ASCII.GetBytes(text));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        [HttpPost("ipn")]
        public IActionResult ReceiveMoMoIPN([FromBody] MoMoIPNRequest request)
        {
            var rawData = $"partnerCode={request.PartnerCode}&orderId={request.OrderId}&requestId={request.RequestId}&amount={request.Amount}&orderInfo={request.OrderInfo}&orderType={request.OrderType}&transId={request.TransId}&resultCode={request.ResultCode}&message={request.Message}&payType={request.PayType}&responseTime={request.ResponseTime}&extraData={request.ExtraData}";
            var signature = GetSignature(rawData, SecretKey);

            if (signature != request.Signature)
            {
                return BadRequest("Invalid signature");
            }

            // TODO: Process the IPN request and update the order status in your database

            return NoContent(); // HTTP 204 No Content
        }


    }
}


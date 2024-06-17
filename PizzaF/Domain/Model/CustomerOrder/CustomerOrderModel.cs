using Domain.Model.CustomerDrink;
using Domain.Model.CustomerPizza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.CustomerOrder
{
    public class CustomerOrderModel
    {
        [JsonPropertyName("id")]
        public int OrderId { get; set; }
        [JsonPropertyName("user-id")]
        public int UserId { get; set; }
        [JsonPropertyName("order-date")]
        public DateTime OrderDate { get; set; }
        [JsonPropertyName("customer-pizzas")]
        public List<CustomerPizzaModel> customerPizzas { get; set; }
        [JsonPropertyName("customer-drinks")]
        public List<CustomerDrinkModel> customerDrinks { get; set; }
        [JsonPropertyName("total-amount")]
        public decimal TotalAmount { get; set; }
    }
}

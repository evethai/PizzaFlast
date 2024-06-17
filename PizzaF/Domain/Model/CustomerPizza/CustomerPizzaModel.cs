using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.CustomerPizza
{
    public class CustomerPizzaModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("order-id")]
        public int OrderId { get; set; }
        [JsonPropertyName("pizza-id")]
        public int PizzaId { get; set; }
        [JsonPropertyName("size-id")]
        public int SizeId { get; set; }
        [JsonPropertyName("topping-id")]
        public int ToppingId { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }

    public class CustomerPizzaResponseModel
    {
        public List<CustomerPizzaModel> customerPizzas { get; set; }
    }
}

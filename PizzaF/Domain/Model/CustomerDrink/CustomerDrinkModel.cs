using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.CustomerDrink
{
    public class CustomerDrinkModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("order-id")]
        public int OrderId { get; set; }
        [JsonPropertyName("drink-id")]
        public int DrinkId { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }

    public class CustomerDrinkResponseModel
    {
        public List<CustomerDrinkModel> customerDrinks { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.Pizza
{
    public class PizzaModel
    {
        [JsonPropertyName("id")]
        public int PizzaId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
    }

    public class PizzaResponseModel
    {
        public int? total { get; set; }
        public int? currentPage { get; set; }
        public List<PizzaModel> pizzas { get; set; }
    }
}

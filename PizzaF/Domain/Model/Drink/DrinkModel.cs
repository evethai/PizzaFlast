using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.Drink
{
    public class DrinkModel
    {
        [JsonPropertyName("id")]
        public int DrinkId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
    }

    public class DrinkResponseModel
    {
        public int? total { get; set; }
        public int? currentPage { get; set; }
        public List<DrinkModel> drinks { get; set; }
    }
}

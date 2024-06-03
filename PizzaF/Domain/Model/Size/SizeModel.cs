using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.Size
{
    public class SizeModel
    {
        [JsonPropertyName("id")]
        public int SizeId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }

    public class SizeResponseModel
    {
        public List<SizeModel> sizes { get; set; }
    }
}

using Domain.Model.Drink;
using Domain.Model.Pizza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.Dashboard
{
    public class DashboardModel
    {
        [JsonPropertyName("total-users")]
        public int TotalUser { get; set; }
        [JsonPropertyName("total-orders")]
        public int TotalOrder { get; set; }
        [JsonPropertyName("total-revenue")]
        public decimal TotalRevenue { get; set; }
        [JsonPropertyName("top-1-pizza-order")]
        public PizzaModel Top1PizzaOrder { get; set; } = null!;
        [JsonPropertyName("top-1-drink-order")]
        public DrinkModel Top1DrinkOrder { get; set; } = null!;
    }
}

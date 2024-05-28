using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Drink
{
    public class DrinkSearchModel
    {
        [FromQuery(Name = "current-page")]
        public int? currentPage { get; set; }
        [FromQuery(Name = "page-size")]
        public int? pageSize { get; set; }
        [FromQuery(Name = "min-price")]
        public decimal? minPrice { get; set; }
        [FromQuery(Name = "max-price")]
        public decimal? maxPrice { get; set; }
        [FromQuery(Name = "name")]
        public string? name { get; set; }
        [FromQuery(Name = "sort-by-price")]
        public bool? sortByPrice { get; set; } = false;
        [FromQuery(Name = "descending")]
        public bool? descending { get; set; } = false;
    }
}

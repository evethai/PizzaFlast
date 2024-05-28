using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Drink
{
    public class DrinkPushModel
    {
        [Required(ErrorMessage = "Id is required.")]
        [FromForm(Name = "id")]
        public int DrinkId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [FromForm(Name = "name")]
        public string Name { get; set; } = string.Empty;
        [FromForm(Name = "description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [FromForm(Name = "price")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [FromForm(Name = "image")]
        public string? Image { get; set; }
    }
}

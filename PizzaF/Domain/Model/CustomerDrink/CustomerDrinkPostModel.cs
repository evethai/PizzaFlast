using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.CustomerDrink
{
    public class CustomerDrinkPostModel
    {
        [Required(ErrorMessage = "DrinkId is required.")]
        [FromForm(Name = "drink-id")]
        public int DrinkId { get; set; }
        [Required(ErrorMessage = "OrderId is required.")]
        [FromForm(Name = "order-id")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        [FromForm(Name = "quantity")]
        public int Quantity { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.CustomerDrink
{
    public class CustomerDrinkPutModel
    {
        [Required(ErrorMessage = "Id is required.")]
        [FromForm(Name = "id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "DrinkId is required.")]
        [FromForm(Name = "drink-id")]
        public int DrinkId { get; set; }
        [Required(ErrorMessage = "OrderId is required.")]
        [FromForm(Name = "order-id")]
        public int OrderId { get; set; }
    }
}

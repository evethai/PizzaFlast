using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.CustomerPizza
{
    public class CustomerPizzaPostModel
    {

        [Required(ErrorMessage = "PizzaId is required.")]
        public int PizzaId { get; set; }
        [Required(ErrorMessage = "Size is required.")]
        public int SizeId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Toppings is required.")]
        public int ToppingId { get; set; } 

    }
}

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
        [Required(ErrorMessage = "OrderId is required.")]
        [FromForm(Name = "orderId")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "PizzaId is required.")]
        [FromForm(Name = "pizzaId")]
        public int PizzaId { get; set; }
        [Required(ErrorMessage = "SizeId is required.")]
        [FromForm(Name = "sizeId")]
        public int SizeId { get; set; }
        [Required(ErrorMessage = "ToppingId is required.")]
        [FromForm(Name = "toppingId")]
        public int ToppingId { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        [FromForm(Name = "quantity")]
        public int Quantity { get; set; }

    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.CustomerPizza
{
    public class CustomerPizzaPutModel
    {
        [Required(ErrorMessage = "Id is required.")]
        [FromForm(Name = "id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "OrderId is required.")]
        [FromForm(Name = "order-id")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "PizzaId is required.")]
        [FromForm(Name = "pizza-id")]
        public int PizzaId { get; set; }
        [Required(ErrorMessage = "SizeId is required.")]
        [FromForm(Name = "size-id")]
        public int SizeId { get; set; }
    }
}

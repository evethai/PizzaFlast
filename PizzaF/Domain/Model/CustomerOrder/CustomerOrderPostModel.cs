using Domain.Model.CustomerDrink;
using Domain.Model.CustomerPizza;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.CustomerOrder
{
    public class CustomerOrderPostModel
    {
        [Required(ErrorMessage = "User-id is required.")]
        [FromForm(Name = "user-id")]
        public int UserId { get; set; }
        [FromForm(Name = "list-customer-pizza")]
        public List<CustomerPizzaPostModel>? customerPizzas { get; set; } = new List<CustomerPizzaPostModel>();
        [FromForm(Name = "list-customer-drink")]
        public List<CustomerDrinkPostModel>? customerDrinks { get; set; } = new List<CustomerDrinkPostModel>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class CustomerPizza
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public int SizeId { get; set; }
        public int ToppingId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public CustomerOrder CustomerOrder { get; set; }
        public Pizza Pizza { get; set; }
        public Size Size { get; set; }
        public Topping Topping { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class CustomerOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public User User { get; set; }
        public virtual ICollection<CustomerDrink> CustomerDrinks { get; set; } = null!;
        public virtual ICollection<CustomerPizza> CustomerPizzas { get; set; } = null!;

    }
}

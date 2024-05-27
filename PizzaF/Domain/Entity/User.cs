using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }
        public string? ProfilePic { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}

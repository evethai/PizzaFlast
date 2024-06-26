﻿using Domain.Enum;
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
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public UserRole Role { get; set; }
        public string? ProfilePic { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}

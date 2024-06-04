using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.User
{
    public class RegisterModel
    {
        [Required]
        [FromForm(Name = "first_name")]
        public string? FirstName { get; set; }
        [Required]
        [FromForm(Name = "last_name")]
        public string? LastName { get; set; }
        [Required]
        [FromForm(Name = "email")]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [FromForm(Name = "password")]
        public string Password { get; set; } = null!;
    }
}

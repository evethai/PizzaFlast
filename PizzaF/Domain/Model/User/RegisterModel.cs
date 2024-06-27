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
        [FromForm(Name = "name")]
        public string? Name { get; set; }

        [Required]
        [FromForm(Name = "email")]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [FromForm(Name = "password")]
        public string Password { get; set; } = null!;
        [Required]
        [FromForm(Name = "phone")]
        public string Phone { get; set; } = null!;
        [Required]
        [FromForm(Name = "address")]
        public string Address { get; set; } = null!;
    }
}

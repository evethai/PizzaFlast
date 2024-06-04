using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.User
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [FromForm(Name = "email")]
        public string? Email { get; set; } = null!;
        [Required]
        [FromForm(Name = "password")]
        public string? Password { get; set; } = null!;
    }

}

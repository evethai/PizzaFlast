using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Size
{
    public class SizePostModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [FromForm(Name = "name")]
        public string Name { get; set; } = null!;
        [FromForm(Name = "description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [FromForm(Name = "price")]
        public decimal Price { get; set; }
    }
}

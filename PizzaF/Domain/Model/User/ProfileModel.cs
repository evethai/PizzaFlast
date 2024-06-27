using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.User
{
    public class ProfileModel
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("date-of-birth")]
        public DateTime DateOfBirth { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
        [JsonPropertyName("profile-pic")]
        public string? ProfilePic { get; set; }
    }
    public class ProfilePutModel
    {
        [Required(ErrorMessage = "Id is required.")]
        [FromForm(Name = "id")]
        public int UserId { get; set; }
        [FromForm(Name = "name")]
        public string? Name { get; set; }

        [FromForm(Name = "date_of_birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [FromForm(Name = "address")]
        public string? Address { get; set; }
        [FromForm(Name = "phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Phone is not valid")]
        public string? Phone { get; set; }
        [FromForm(Name = "profile_pic")]
        public string? ProfilePic { get; set; }
    }
}

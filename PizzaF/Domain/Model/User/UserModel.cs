using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.User
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; } = null!;
        public int Role { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
    }


}

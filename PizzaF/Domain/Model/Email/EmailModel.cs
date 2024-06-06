using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Email
{
    public class EmailModel
    {
        public string To { get; set; } = string.Empty;
        public string VerificationLink { get; set; } = string.Empty;
    }
}

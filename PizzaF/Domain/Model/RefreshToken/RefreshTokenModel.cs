using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.RefreshToken
{
    public class RefreshTokenModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
    }
}

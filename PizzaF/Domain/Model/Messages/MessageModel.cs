using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Messages
{
    public class MessageModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string? Content { get; set; }
    }
}

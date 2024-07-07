using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Notification
{
    public class NotificationPostModel
    {
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
    }
}

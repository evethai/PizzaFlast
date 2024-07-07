using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Model.Notification
{
    public class NotificationModel
    {
        [JsonPropertyName("id")]
        public int NotificationId { get; set; }
        [JsonPropertyName("user-id")]
        public int UserId { get; set; }
        [JsonPropertyName("content")]
        public string? Content { get; set; }
        [JsonPropertyName("received-at")]
        public DateTime ReceivedAt { get; set; }
        [JsonPropertyName("status")]
        public NotiStatus Status { get; set; }
    }
}

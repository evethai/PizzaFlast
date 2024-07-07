using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.CustomerOrder
{
    public class CusOrderHistoryModel
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Image { get; set; } = null!;
    }
}

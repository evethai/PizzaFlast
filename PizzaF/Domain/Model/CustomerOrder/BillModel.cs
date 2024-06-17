using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.CustomerOrder
{
    public class BillModel
    {
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }
        public List<BillDetailModel> BillDetails { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class BillDetailModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

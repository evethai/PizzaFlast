using Domain.Enum;
using Domain.Model;
using Domain.Model.CustomerOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface ICustomerOrderService
    {
        Task<ResponseModel> CreateOrder(CustomerOrderPostModel model);
        Task<BillModel> GetBillByOrderId(int id);
        Task<ResponseModel> UpdateOrder(int orderId, OrderStatus status);
        Task<IEnumerable<CusOrderHistoryModel>> GetHistoryOrderByUserId(int userId);
    }
}

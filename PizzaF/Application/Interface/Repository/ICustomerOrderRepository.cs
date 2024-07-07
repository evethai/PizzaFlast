using Domain.Entity;
using Domain.Model.CustomerOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface ICustomerOrderRepository: IGenericRepository<CustomerOrder>
    {
        Task<CustomerOrderModel> CreateCustomerOrder(CustomerOrderPostModel customerOrder);
        Task<BillModel> GetCustomerOrderById(int id);
        Task<IEnumerable<CusOrderHistoryModel>> GetHistoryOrderByUserId(int userId);

    }
}

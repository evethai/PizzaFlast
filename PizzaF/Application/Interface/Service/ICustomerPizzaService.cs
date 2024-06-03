using Domain.Model;
using Domain.Model.CustomerPizza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface ICustomerPizzaService
    {
        Task<CustomerPizzaResponseModel> GetCustomerPizzasByOrderId(int id);
        Task<CustomerPizzaModel> GetCustomerPizzaById(int id);
        Task<ResponseModel> CreateCustomerPizza(CustomerPizzaPostModel customerPizzaModel);
        Task<ResponseModel> UpdateCustomerPizza(CustomerPizzaPutModel customerPizzaModel);

    }
}

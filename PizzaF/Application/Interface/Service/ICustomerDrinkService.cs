using Domain.Model;
using Domain.Model.CustomerDrink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface ICustomerDrinkService
    {
        Task<CustomerDrinkResponseModel> GetCustomerDrinkByOrderId(int id);
        Task<CustomerDrinkModel> GetCustomerDrinkById(int id);
        Task<ResponseModel> CreateCustomerDrink(CustomerDrinkPostModel customerDrinkModel);
        Task<ResponseModel> UpdateCustomerDrink(CustomerDrinkPutModel customerDrinkModel);
    }
}

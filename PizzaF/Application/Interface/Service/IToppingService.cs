using Domain.Model;
using Domain.Model.Topping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IToppingService
    {
        Task<ToppingResponseModel> GetToppings();
        Task<ToppingModel> GetToppingById(int id);
        Task<ResponseModel> CreateTopping(ToppingPostModel toppingModel);
        Task<ResponseModel> UpdateTopping(ToppingPutModel toppingModel);
        Task<ResponseModel> DeleteTopping(int id);
    }
}

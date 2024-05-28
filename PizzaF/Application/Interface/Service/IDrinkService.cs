using Domain.Model.Drink;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IDrinkService
    {
        Task<DrinkResponseModel> GetListDrinkAsync(DrinkSearchModel searchModel);
        Task<DrinkModel> GetDrinkByIdAsync(int DrinkId);
        Task<ResponseModel> CreateDrinkAsync(DrinkPostModel model);
        Task<ResponseModel> UpdateDrinkAsync(DrinkPushModel model);
    }
}

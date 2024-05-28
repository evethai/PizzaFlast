using Domain.Model;
using Domain.Model.Pizza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IPizzaService
    {
        Task<PizzaResponseModel> GetListPizzaAsync(PizzaSearchModel searchModel);
        Task<PizzaModel> GetPizzaByIdAsync(int pizzaId);
        Task<ResponseModel> CreatePizzaAsync(PizzaPostModel model);
        Task<ResponseModel> UpdatePizzaAsync(PizzaPushModel model);
    }
}

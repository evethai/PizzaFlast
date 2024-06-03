using Domain.Entity;
using Domain.Model.CustomerPizza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface ICustomerPizzaRepository: IGenericRepository<CustomerPizza>
    {
        Task<List<CustomerPizza>> GetByOrderId(int id);
    }
}

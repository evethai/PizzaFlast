using Application.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IPizzaRepository PizzaRepository { get; }
        IDrinkRepository DrinkRepository { get; }
        ISizeRepository SizeRepository { get; }
        IToppingRepository ToppingRepository { get; }
        ICustomerPizzaRepository CustomerPizzaRepository { get; }
        ICustomerDrinkRepository CustomerDrinkRepository { get; }

        int Save();
    }
}

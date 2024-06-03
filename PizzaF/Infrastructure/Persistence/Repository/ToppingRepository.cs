using Application.Interface.Repository;
using Domain.Entity;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class ToppingRepository : GenericRepository<Topping>, IToppingRepository
    {
        public ToppingRepository(PizzaFDbContext context) : base(context) 
        { 
        }

    }

}

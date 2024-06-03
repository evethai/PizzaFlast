using Application.Interface.Repository;
using Domain.Entity;
using Domain.Model.CustomerPizza;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class CustomerPizzaRepository : GenericRepository<CustomerPizza>, ICustomerPizzaRepository
    {
        private readonly PizzaFDbContext _context;
        public CustomerPizzaRepository(PizzaFDbContext context) : base(context)
        {
            _context = context;
        }

        async Task<List<CustomerPizza>> ICustomerPizzaRepository.GetByOrderId(int id)
        {
            var list = await _context.CustomerPizzas.Where(x => x.OrderId == id).ToListAsync();
            return list;
        }
    }   

}

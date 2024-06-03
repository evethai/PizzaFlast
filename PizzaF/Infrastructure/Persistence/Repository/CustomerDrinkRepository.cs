using Application.Interface.Repository;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class CustomerDrinkRepository : GenericRepository<CustomerDrink>, ICustomerDrinkRepository
    {
        private readonly PizzaFDbContext _context;
        public CustomerDrinkRepository(PizzaFDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CustomerDrink>> GetListByOrderId(int orderId)
        {
            var list = await _context.CustomerDrinks.Where(e=>e.OrderId == orderId).ToListAsync();
            return list;
        }
    }

}

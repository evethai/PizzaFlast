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
    public class SizeRepository : GenericRepository<Size>, ISizeRepository
    {
        public SizeRepository(PizzaFDbContext context) : base(context)
        {
        }
    }
}

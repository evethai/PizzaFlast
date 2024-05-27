using Domain.Entity;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IPizzaRepository : IGenericRepository<Pizza>
    {
        (Expression<Func<Pizza, bool>> filter, Func<IQueryable<Pizza>, IOrderedQueryable<Pizza>> orderBy) BuildFilterAndOrderBy(PizzaSearchModel searchModel);
    }
}

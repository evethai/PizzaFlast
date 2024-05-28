using Domain.Entity;
using Domain.Model.Drink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IDrinkRepository : IGenericRepository<Drink>
    {
        (Expression<Func<Drink, bool>> filter, Func<IQueryable<Drink>, IOrderedQueryable<Drink>> orderBy) BuildFilterAndOrderBy(DrinkSearchModel searchModel);

    }
}

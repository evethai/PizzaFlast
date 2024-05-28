using Application.Common.Extensions;
using Application.Interface.Repository;
using Domain.Entity;
using Domain.Model.Drink;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class DrinkRepository : GenericRepository<Drink>, IDrinkRepository
    {
        public DrinkRepository(PizzaFDbContext context) : base(context)
        {
        }

        public (Expression<Func<Drink, bool>> filter, Func<IQueryable<Drink>, IOrderedQueryable<Drink>> orderBy) BuildFilterAndOrderBy(DrinkSearchModel searchModel)
        {
            Expression<Func<Drink, bool>> filter = p => true;
            Func<IQueryable<Drink>, IOrderedQueryable<Drink>> orderBy = null;

            if (searchModel.minPrice.HasValue)
            {
                filter = filter.And(p => p.Price >= searchModel.minPrice.Value);
            }
            if (searchModel.maxPrice.HasValue)
            {
                filter = filter.And(p => p.Price <= searchModel.maxPrice.Value);
            }
            if (!string.IsNullOrEmpty(searchModel.name))
            {
                filter = filter.And(p => p.Name.Contains(searchModel.name));
            }

            if(searchModel.sortByPrice == true)
            {
                orderBy = p => searchModel.descending == true ?
                               p.OrderByDescending(x => x.Price) :
                               p.OrderBy(x => x.Price);
            }

            return (filter, orderBy);
        }
    }

}

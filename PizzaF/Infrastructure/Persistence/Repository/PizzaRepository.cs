using Application.Common.Extensions;
using Application.Interface;
using Application.Interface.Repository;
using AutoMapper;
using Domain.Entity;
using Domain.Model;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class PizzaRepository : GenericRepository<Pizza>, IPizzaRepository
    {
        public PizzaRepository(PizzaFDbContext context) : base(context)
        {
        }

        public (Expression<Func<Pizza, bool>> filter, Func<IQueryable<Pizza>, IOrderedQueryable<Pizza>> orderBy) BuildFilterAndOrderBy(PizzaSearchModel searchModel)
        {
            Expression<Func<Pizza, bool>> filter = p => true;
            Func<IQueryable<Pizza>, IOrderedQueryable<Pizza>> orderBy = null;

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

            if (searchModel.sortByPrice == true)
            {
                orderBy = p => searchModel.descending == true ?
                               p.OrderByDescending(x => x.Price) :
                               p.OrderBy(x => x.Price);
            }
            return (filter, orderBy);
        }


    }
}

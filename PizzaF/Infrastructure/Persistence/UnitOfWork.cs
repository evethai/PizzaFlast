using Application.Interface;
using Application.Interface.Repository;
using Infrastructure.Data;
using Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PizzaFDbContext _context;
        private PizzaRepository _pizzaRepository;
        private DrinkRepository _drinkRepository;

        public UnitOfWork(PizzaFDbContext context)
        {
            _context = context;
        }

        public IPizzaRepository PizzaRepository
        {
            get
            {
                if (_pizzaRepository == null)
                {
                    _pizzaRepository = new PizzaRepository(_context);
                }
                return _pizzaRepository;
            }
        }

        public IDrinkRepository DrinkRepository
        {
            get
            {
                if(_drinkRepository == null)
                {
                    _drinkRepository = new DrinkRepository(_context);
                }
                return _drinkRepository;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    
}

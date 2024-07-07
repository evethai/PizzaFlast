using Application.Interface;
using Application.Interface.Repository;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private PizzaRepository _pizzaRepository;
        private DrinkRepository _drinkRepository;
        private SizeRepository _sizeRepository;
        private ToppingRepository _toppingRepository;
        private CustomerPizzaRepository _customerPizzaRepository;
        private CustomerDrinkRepository _customerDrinkRepository;
        private UserRepository _userRepository;
        private RefreshTokenRepository _refreshTokenRepository;
        private MessageRepository _messageRepository;
        private CustomerOrderRepository _customerOrderRepository;
        private NotificationRepository _notificationRepository;

        public UnitOfWork(PizzaFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public ISizeRepository SizeRepository
        {
            get
            {
                if (_sizeRepository == null)
                {
                    _sizeRepository = new SizeRepository(_context);
                }
                return _sizeRepository;
            }
        }

        public IToppingRepository ToppingRepository
        {
            get
            {
                if (_toppingRepository == null)
                {
                    _toppingRepository = new ToppingRepository(_context);
                }
                return _toppingRepository;
            }
        }

        public ICustomerPizzaRepository CustomerPizzaRepository
        {
            get
            {
                if (_customerPizzaRepository == null)
                {
                    _customerPizzaRepository = new CustomerPizzaRepository(_context);
                }
                return _customerPizzaRepository;
            }
        }

        public ICustomerDrinkRepository CustomerDrinkRepository
        {
            get
            {
                if (_customerDrinkRepository == null)
                {
                    _customerDrinkRepository = new CustomerDrinkRepository(_context);
                }
                return _customerDrinkRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context, _mapper);
                }
                return _userRepository;
            }
        }

        public IRefreshTokenRepository RefreshTokenRepository
        {
            get
            {
                if (_refreshTokenRepository == null)
                {
                    _refreshTokenRepository = new RefreshTokenRepository(_context);
                }
                return _refreshTokenRepository;
            }
        }

        public IMessageRepository MessageRepository
        {
            get
            {
                if (_messageRepository == null)
                {
                    _messageRepository = new MessageRepository(_context);
                }
                return _messageRepository;
            }
        }

        public ICustomerOrderRepository CustomerOrderRepository
        {
            get
            {
                if (_customerOrderRepository == null)
                {
                    _customerOrderRepository = new CustomerOrderRepository(_context, _mapper);
                }
                return _customerOrderRepository;
            }
        }

        public INotificationRepository NotificationRepository
        {
            get
            {
                if (_notificationRepository == null)
                {
                    _notificationRepository = new NotificationRepository(_context);
                }
                return _notificationRepository;
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

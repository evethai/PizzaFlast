using Application.Common.Extensions;
using Application.Interface.Repository;
using AutoMapper;
using Domain.Entity;
using Domain.Enum;
using Domain.Model.Dashboard;
using Domain.Model.Drink;
using Domain.Model.Pizza;
using Domain.Model.User;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly PizzaFDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(PizzaFDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public (Expression<Func<User, bool>> filter, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy) BuildFilterAndOrderBy(UsersSearchModel searchModel)
        {
            Expression<Func<User, bool>> filter = p=>p.Role == UserRole.User && p.VerifiedAt != null;
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null;
            if (!string.IsNullOrEmpty(searchModel.name))
            {
                filter = filter.And(p => p.Name.Contains(searchModel.name) || p.Email.Contains(searchModel.name));
            }
            if(searchModel.sortByDateVerify == true)
            {
                orderBy = p => searchModel.descending == true ?
                               p.OrderByDescending(x => x.VerifiedAt) :
                               p.OrderBy(x => x.VerifiedAt);
            }
            return (filter, orderBy);
        }

        public async Task<DashboardModel> getDashBoard()
        {
            int numberUser = _context.Users.Where(p => p.Role == UserRole.User && p.VerifiedAt != null).Count();
            int numberOrder = _context.CustomerOrders.Count();
            decimal totalRevenue = _context.CustomerOrders.Sum(p => p.TotalAmount);
            var pizzaId = _context.CustomerPizzas.GroupBy(p => p.PizzaId).Select(p => new { pizzaId = p.Key, count = p.Count() }).OrderByDescending(p => p.count).FirstOrDefault();
            var pizza = _context.Pizzas.Where(p => p.PizzaId == pizzaId.pizzaId).FirstOrDefault();
            var drinkId = _context.CustomerDrinks.GroupBy(p => p.DrinkId).Select(p => new { drinkId = p.Key, count = p.Count() }).OrderByDescending(p => p.count).FirstOrDefault();
            var drink = _context.Drinks.Where(p => p.DrinkId == drinkId.drinkId).FirstOrDefault();

            return new DashboardModel
            {
                TotalUser = numberUser,
                TotalOrder = numberOrder,
                TotalRevenue = totalRevenue,
                Top1PizzaOrder = _mapper.Map<PizzaModel>(pizza),
                Top1DrinkOrder = _mapper.Map<DrinkModel>(drink)
            };
        }

        public async Task<User> Login(LoginModel loginModel)
        {
            var user = _context.Users.Where(p => p.Email.Equals(loginModel.Email)).FirstOrDefault();
            if (user != null )
            {
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password);
                if(isValidPassword)
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<int> RegisterUser(RegisterModel registerModel)
        {
            var userExist = _context.Users.Where(p => p.Email.Equals(registerModel.Email)).FirstOrDefault();
            if (userExist != null)
            {
                return -1;
            }

            var user = _mapper.Map<User>(registerModel);
            user.Role = UserRole.User;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.VerificationToken = CreateRandomToken();

            var newUser = _context.Users.Add(user);

            _context.SaveChanges();

            return newUser.Entity.UserId;
        }

        public async Task<User> UpdateUserProfile(ProfilePutModel model)
        {
            var user =  _context.Users.Where(p => p.UserId.Equals(model.UserId)).FirstOrDefault();
            if (user != null)
            {
                user = _mapper.Map(model, user);
                _context.SaveChanges();
                return user;
            }
            return null;
        }

        public async Task<bool> Verify(string token)
        {
            var user = _context.Users.Where(p => p.VerificationToken.Equals(token)).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            user.VerifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

    }
}

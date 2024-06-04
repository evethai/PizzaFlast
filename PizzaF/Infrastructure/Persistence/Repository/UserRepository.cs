using Application.Interface.Repository;
using AutoMapper;
using Domain.Entity;
using Domain.Enum;
using Domain.Model.User;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly PizzaFDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(PizzaFDbContext context,IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }




        public async Task<User> Login(LoginModel loginModel)
        {
            var user =  _context.Users.Where(p => p.Email.Equals(loginModel.Email) &&
            p.Password.Equals(loginModel.Password)).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<User> RegisterUser(RegisterModel loginModel)
        {
            var user = _mapper.Map<User>(loginModel);
            user.Role = UserRole.User;
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;

        }

    }
}

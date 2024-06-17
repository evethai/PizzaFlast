using Application.Interface.Repository;
using AutoMapper;
using Domain.Entity;
using Domain.Enum;
using Domain.Model.User;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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




        public async Task<User> Login(LoginModel loginModel)
        {
            var user = _context.Users.Where(p => p.Email.Equals(loginModel.Email)).FirstOrDefault();

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginModel.Password, user.Password);

            if (user != null && isValidPassword)
            {
                return user;
            }

            return null;
        }

        public async Task<bool> RegisterUser(RegisterModel loginModel)
        {
            var userExist = _context.Users.Where(p => p.Email.Equals(loginModel.Email)).FirstOrDefault();
            if (userExist != null)
            {
                return false;
            }

            var user = _mapper.Map<User>(loginModel);
            user.Role = UserRole.User;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.VerificationToken = CreateRandomToken();

            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
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

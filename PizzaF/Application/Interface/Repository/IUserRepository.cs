using Domain.Entity;
using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {

        Task<User> Login(LoginModel loginModel);
        Task<bool> RegisterUser(RegisterModel loginModel);
        Task<bool> Verify(string token);
    }
}

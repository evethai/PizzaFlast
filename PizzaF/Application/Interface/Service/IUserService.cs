using Domain.Model.RefreshToken;
using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IUserService
    {
        Task<ResponseTokenModel> GenerateTokenString(UserModel model);
        Task<ResponseTokenModel> Login(LoginModel model);
        Task<bool> RegisterUser(RegisterModel model);
        Task<RefreshTokenModel> CreateRefreshToken(ResponseTokenModel model);

    }
}

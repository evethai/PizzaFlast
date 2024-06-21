using Domain.Model;
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
        Task<UserModel> Login(LoginModel model);
        Task<bool> RegisterUser(RegisterModel model);
        Task<RefreshTokenModel> CreateRefreshToken(ResponseTokenModel model);
        Task<bool> Verify (string token);
        Task<ProfileModel> GetUserProfile(int id);
        Task<ResponseModel> UpdateProfile(ProfilePutModel model);
        Task<bool> Logout(string token);

    }
}

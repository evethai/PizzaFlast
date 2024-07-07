using Domain.Model;
using Domain.Model.Dashboard;
using Domain.Model.Drink;
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
        Task<int> RegisterUser(RegisterModel model);
        Task<RefreshTokenModel> CreateRefreshToken(ResponseTokenModel model);
        Task<bool> Verify (string token);
        Task<ProfileModel> GetUserProfile(int id);
        Task<ResponseModel> UpdateProfile(ProfilePutModel model);
        Task<RefreshTokenModel> RevokeToken(string token);
        Task<UsersResponseModel> GetListUserAsync(UsersSearchModel searchModel);
        Task<DashboardModel> getDashBoard();
    }
}

using Domain.Entity;
using Domain.Model.Dashboard;
using Domain.Model.Drink;
using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {

        Task<User> Login(LoginModel loginModel);
        Task<int> RegisterUser(RegisterModel loginModel);
        Task<bool> Verify(string token);
        Task<User> UpdateUserProfile(ProfilePutModel model);
        (Expression<Func<User, bool>> filter, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy) BuildFilterAndOrderBy(UsersSearchModel searchModel);
        Task<DashboardModel> getDashBoard();
    }
}

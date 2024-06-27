using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken> ExitedToken(string refreshToken);
        Task<RefreshToken> GetByJwtIdAsync(string jwtId);
        Task<bool> IsTokenRevokedAsync(string jwtId);
    }
}

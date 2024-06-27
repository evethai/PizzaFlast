using Application.Interface.Repository;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly PizzaFDbContext _context;
        public RefreshTokenRepository(PizzaFDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RefreshToken> ExitedToken(string refreshToken)
        {
            var token = _context.RefreshTokens.Where(p => p.Token.Equals(refreshToken)).FirstOrDefault();
            if (token == null)
            {
                return null;
            }
            return token;
        }
        public async Task<RefreshToken> GetByJwtIdAsync(string jwtId)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.JwtId.Equals(jwtId));
        }

        public async Task<bool> IsTokenRevokedAsync(string jwtId)
        {
            return await _context.RefreshTokens.AnyAsync(rt => rt.JwtId == jwtId && rt.IsRevoked == false);
        }
    }

}

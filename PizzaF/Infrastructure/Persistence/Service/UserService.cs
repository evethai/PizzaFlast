using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entity;
using Domain.Model;
using Domain.Model.Dashboard;
using Domain.Model.RefreshToken;
using Domain.Model.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Persistence.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
        }
        public async Task<ResponseTokenModel> GenerateTokenString(UserModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()),
                new Claim(ClaimTypes.Role,model.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshTokenString();

            var refreshTokenModel = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = refreshToken,
                JwtId = token.Id,
                UserId = model.UserId,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(60),
            };

            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshTokenModel);
            _unitOfWork.Save();

            return new ResponseTokenModel
            {
                Token = accessToken,
                RefreshToken = refreshToken
            };

        }

        private string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];

            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        public async Task<UserModel> Login(LoginModel model)
        {
            var user = _unitOfWork.UserRepository.Login(model);
            if (user.Result == null)
            {
                return null;
            }
            var result = _mapper.Map<UserModel>(user.Result);
            return result;
        }

        public async Task<RefreshTokenModel> RevokeToken(string token)
        {
            var (principal, jwtSecurityToken, errorMessage) = ValidateToken(token);
            if (principal == null)
            {
                return new RefreshTokenModel
                {
                    IsSuccess = false,
                    Message = errorMessage
                };
            }

            var jti = principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var refreshToken = await _unitOfWork.RefreshTokenRepository.GetByJwtIdAsync(jti);
            if (refreshToken == null)
            {
                return new RefreshTokenModel
                {
                    IsSuccess = false,
                    Message = "Refresh token not found"
                };
            }

            refreshToken.IsRevoked = true;
            await _unitOfWork.RefreshTokenRepository.UpdateAsync(refreshToken);
            _unitOfWork.Save();

            return new RefreshTokenModel
            {
                IsSuccess = true,
                Message = "Token has been revoked"
            };
        }


        public async Task<int> RegisterUser(RegisterModel model)
        {
            var userId = await _unitOfWork.UserRepository.RegisterUser(model);
            return userId;
        }

        public async Task<RefreshTokenModel> CreateRefreshToken(ResponseTokenModel model)
        {
            var (principal, jwtSecurityToken, errorMessage) = ValidateToken(model.Token);
            if (principal == null)
            {
                return new RefreshTokenModel
                {
                    IsSuccess = false,
                    Message = errorMessage
                };
            }

            var expiryDateUnix = long.Parse(principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = ConvertUnixTimeToDateTime(expiryDateUnix);
            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new RefreshTokenModel
                {
                    IsSuccess = false,
                    Message = "This token hasn't expired yet"
                };
            }

            var exitedToken = await _unitOfWork.RefreshTokenRepository.ExitedToken(model.RefreshToken);
            if (exitedToken == null)
            {
                return new RefreshTokenModel
                {
                    IsSuccess = false,
                    Message = "This refresh token does not exist"
                };
            }

            if (exitedToken.IsUsed)
            {
                return new RefreshTokenModel
                {
                    IsSuccess = false,
                    Message = "Refresh token has been used"
                };
            }

            if (exitedToken.IsRevoked)
            {
                return new RefreshTokenModel
                {
                    IsSuccess = false,
                    Message = "Refresh token has been revoked"
                };
            }

            var jti = principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (exitedToken.JwtId != jti)
            {
                return new RefreshTokenModel
                {
                    IsSuccess = false,
                    Message = "This refresh token does not match this JWT token"
                };
            }

            exitedToken.IsRevoked = true;
            exitedToken.IsUsed = true;
            await _unitOfWork.RefreshTokenRepository.UpdateAsync(exitedToken);
            _unitOfWork.Save();

            var user = await _unitOfWork.UserRepository.GetByIdAsync(exitedToken.UserId);
            var mapperUser = _mapper.Map<UserModel>(user);
            var newToken = await GenerateTokenString(mapperUser);

            return new RefreshTokenModel
            {
                IsSuccess = true,
                Message = "Create new token success"
            };
        }
        private DateTime ConvertUnixTimeToDateTime(long unixTime)
        {
            var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTime);
            return dateTimeOffset.UtcDateTime;
        }

        public async Task<bool> Verify(string token)
        {
            var isVerify = await _unitOfWork.UserRepository.Verify(token);
            if(isVerify == false)
            {
               return false;
            }
            return true;
        }

        public async Task<ProfileModel> GetUserProfile(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            var result = _mapper.Map<ProfileModel>(user);

            return result;
        }

        public async Task<ResponseModel> UpdateProfile(ProfilePutModel model)
        {   
            var result = await _unitOfWork.UserRepository.UpdateUserProfile(model);
            var user = _mapper.Map<ProfileModel>(result);
            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Profile updated successfully.",
                Data = user
            };
        }

        private (ClaimsPrincipal principal, JwtSecurityToken jwtSecurityToken, string errorMessage) ValidateToken(string token)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var paramTokenValid = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false
            };

            try
            {
                var principal = jwtTokenHandler.ValidateToken(token, paramTokenValid, out var securityToken);

                if (securityToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return (null, null, "Invalid alg token");
                    }
                    return (principal, jwtSecurityToken, null);
                }
            }
            catch (Exception ex)
            {
                return (null, null, ex.Message);
            }

            return (null, null, "Invalid token");
        }

        public async Task<UsersResponseModel> GetListUserAsync(UsersSearchModel searchModel)
        {
            var (filter, orderBy) = _unitOfWork.UserRepository.BuildFilterAndOrderBy(searchModel);
            var users = await _unitOfWork.UserRepository.GetByConditionAsync(filter, orderBy, pageIndex: searchModel.currentPage, pageSize: searchModel.pageSize);
            var total = await _unitOfWork.UserRepository.CountAsync(filter);
            var listUser = _mapper.Map<List<UserModel>>(users);
            return new UsersResponseModel
            {
                total = total,
                currentPage = searchModel.currentPage,
                users = listUser,
            };
        }

        public async Task<DashboardModel> getDashBoard()
        {
            var result = await _unitOfWork.UserRepository.getDashBoard();
            return result;
        }
    }



}

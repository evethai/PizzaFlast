using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entity;
using Domain.Model;
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
                new Claim(ClaimTypes.Email, model.Email),
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

        public async Task<bool> Logout(string token)
        {
            var logout = await _unitOfWork.UserRepository.Logout(token);
            if (logout == false)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RegisterUser(RegisterModel model)
        {
            var user = await _unitOfWork.UserRepository.RegisterUser(model);
            if(user == false )
            {
                return false;
            }
            return true;

        }

        public async Task<RefreshTokenModel> CreateRefreshToken(ResponseTokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var paramTokenValid = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value)),

                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false
            };
            try
            {
                //check 1: accept token is valid
                var principal = jwtTokenHandler.ValidateToken(model.Token, paramTokenValid, out var securityToken);

                //check 2: alg
                if (securityToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return new RefreshTokenModel
                        {
                            IsSuccess = false,
                            Message = "Invalid alg token"
                        };
                    }
                }

                //check 3: check accept token expired?
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

                //check 4: check refresh token exited in DB
                var exitedToke = await _unitOfWork.RefreshTokenRepository.ExitedToken(model.RefreshToken);
                if (exitedToke == null)
                {
                    return new RefreshTokenModel
                    {
                        IsSuccess = false,
                        Message = "This refresh token is not exited"
                    };
                }

                //check 5: check refresh token is used/revoked?
                if(exitedToke.IsUsed)
                {
                    return new RefreshTokenModel
                    {
                        IsSuccess = false,
                        Message = "Refresh token has been used"
                    };
                }
                if (exitedToke.IsRevoked)
                {
                    return new RefreshTokenModel
                    {
                        IsSuccess = false,
                        Message = "Refresh token has been revoked"
                    };
                }

                //check 6: check Accept id equal jwtId in stored
                var jti = principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (exitedToke.JwtId != jti)
                {
                    return new RefreshTokenModel
                    {
                        IsSuccess = false,
                        Message = "This refresh token does not match this JWT token"
                    };
                }

                //update token is used
                exitedToke.IsRevoked = true;
                exitedToke.IsUsed = true;
                await _unitOfWork.RefreshTokenRepository.UpdateAsync(exitedToke);
                _unitOfWork.Save();

                //create new refresh token
                var user = await _unitOfWork.UserRepository.GetByIdAsync(exitedToke.UserId);
                var mapperUser = _mapper.Map<UserModel>(user);
                var newToken = await GenerateTokenString(mapperUser);


                return new RefreshTokenModel
                {
                    IsSuccess = true,
                    Message = "Create new token success"
                };


            }
            catch (Exception ex)
            {
               return new RefreshTokenModel
               {
                   IsSuccess = false,
                   Message = ex.Message
               };
            }
        }
        private DateTime ConvertUnixTimeToDateTime(long unixTime)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTime).ToUniversalTime();
            return dateTime;
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
    }



}

using Application.Interface.Service;
using Domain.Entity;
using Domain.Model;
using Domain.Model.Email;
using Domain.Model.RefreshToken;
using Domain.Model.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListUserAsync([FromQuery] UsersSearchModel searchModel)
        {
            try
            {
                var result = await _userService.GetListUserAsync(searchModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _userService.Login(model);
                if (result == null)
                {
                    return Unauthorized("Invalid username or password");
                }
                if (result.VerifiedAt == null)
                {
                    return BadRequest("Not verified!");
                }
                var acceptToken = await _userService.GenerateTokenString(result);
                return Ok(acceptToken);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userId = await _userService.RegisterUser(model);
                if (userId <= 0)
                {
                    return BadRequest("Account with email has exited!!!");
                }
                var mail = await _emailService.SendEmail(userId);
                if (mail == false)
                {
                    return BadRequest("Send mail failed");
                }
                return Ok(new ResponseModel { IsSuccess = true, Message= "Sign Up Success! Check mail to verify" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(ResponseTokenModel model)
        {
            try
            {
                var result = await _userService.CreateRefreshToken(model);
                if (!result.IsSuccess)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            try
            {
                var result = await _userService.Verify(token);
                if (result == false)
                {
                    return BadRequest("Token is invalid");
                }
                return Ok("User verified!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("send-mail-verify")]
        public  async Task<IActionResult> SendMailToVerify(int userId)
        {
            try
            {
                var result =  await _emailService.SendEmail(userId);
                if(result == false)
                {
                    return BadRequest("Send mail failed");
                }
                return Ok("Send mail success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            try
            {
                var result = await _userService.GetUserProfile(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] ProfilePutModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _userService.UpdateProfile(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string token)
        {
            try
            {
                var result = await _userService.RevokeToken(token);
                if (!result.IsSuccess)
                {
                    return BadRequest("token is not valid");
                }

                return Ok("Logout successful!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashBoard()
        {
            try
            {
                var result = await _userService.getDashBoard();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

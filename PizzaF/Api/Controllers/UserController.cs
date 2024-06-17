using Application.Interface.Service;
using Domain.Model.Email;
using Domain.Model.RefreshToken;
using Domain.Model.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                var result = await _userService.RegisterUser(model);
                if (result == false)
                {
                    return BadRequest("Account with email has exited!!!");
                }
                return Ok("Register success");
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
        public  async Task<IActionResult> SendMailToVerify(EmailModel model)
        {
            try
            {
                var result =  await _emailService.SendEmail(model);
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
    }
}

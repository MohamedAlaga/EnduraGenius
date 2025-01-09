using System.Security.Claims;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.EmailSenderRepository;
using EnduraGenius.API.Repositories.TokenRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace EnduraGenius.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IEmailSender _emailSender;
        public AuthController(UserManager<User> userManager,ITokenRepository tokenRepository, IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
            this._emailSender = emailSender;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var idetityUser = new User
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.Email,
                Points = 0,
                WeightInKg = registerRequestDto.WeightInKg,
                TallInCm = registerRequestDto.TallInCm,
                Age = registerRequestDto.Age,
                IsMale = registerRequestDto.IsMale
            };
            var identityResult = await _userManager.CreateAsync(idetityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(idetityUser, "user");

                if (identityResult.Succeeded)
                {
                    return Ok(identityResult);
                }
            }

            return BadRequest("invalid user data :" + identityResult.Errors.ToList()[0].Description);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.EmailAdress);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginRequestDto.Password))
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPassword)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("invalid login data");
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordDto forgotPasswordRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordRequestDto.Email);
            if (user == null)
            {
                return BadRequest("invalid Request");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", forgotPasswordRequestDto.Email }
            };
            var callbackUrl = QueryHelpers.AddQueryString(forgotPasswordRequestDto.ClientURI, param!);
            await _emailSender.SendEmailAsync(user.Email!, "Reset Password Token", callbackUrl);
            return Ok();
        }
        [HttpPost]
        [Route("ForgotPassword/tokenOnly")]
        public async Task<IActionResult> ForgotPasswordToken([FromBody] ForgetPasswordDto forgotPasswordRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(forgotPasswordRequestDto.Email);
            if (user == null)
            {
                return BadRequest("invalid Request");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailSender.SendEmailAsync(user.Email!, "Reset Password Token", "your token is :" + token + "\nfor email : " + forgotPasswordRequestDto.Email);
            return Ok();
        }
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return BadRequest("invalid Request");
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (resetPassResult.Succeeded)
            {
                return Ok();
            }
            return BadRequest("bad Request");
        }

        [HttpGet]
        [Route("test")]
        // test email sender
        public async Task<IActionResult> Test()
        {
            await _emailSender.SendEmailAsync("2c95c36ba0@emailawb.pro", "test","this email to test email sender");
            return Ok();
        }
    }
}
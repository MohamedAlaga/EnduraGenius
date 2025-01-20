using System.Security.Claims;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.EmailSenderRepository;
using EnduraGenius.API.Repositories.TokenRepositories;
using EnduraGenius.API.Repositories.WorkoutsRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace EnduraGenius.API.Controllers
{
    /// <summary>
    /// this controller is responsible for the authentication of the user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IEmailSender _emailSender;
        /// <summary>
        /// Constructor for AuthController
        /// </summary>
        /// <param name="userManager"> user manger service</param>
        /// <param name="tokenRepository"> Token repository that handles the JWT creation</param>
        /// <param name="emailSender"> email sending service</param>
        public AuthController(UserManager<User> userManager,ITokenRepository tokenRepository, IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
            this._emailSender = emailSender;
        }
        /// <summary>
        /// Register new user account
        /// </summary>
        /// <param name="registerRequestDto">DTO Contains User Data</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the registration operation:
        /// - Returns a 200 OK response if registration is successful.
        /// - Returns a 400 Bad Request response if the user data is invalid.
        /// </returns>
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

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginRequestDto">DTO contains user data</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the Login operation:
        /// - Returns a 200 OK response if registration is successful with the JWT Token.
        /// - Returns a 400 Bad Request response if the user data is invalid.
        /// </returns>
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
        /// <summary>
        /// send reset password link to user email
        /// </summary>
        /// <param name="forgotPasswordRequestDto"> DTO to handle user data</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response if Email Sent succefully to the user.
        /// - Returns a 400 Bad Request response if the user data is invalid.
        /// </returns>
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
        /// <summary>
        /// send reset password token to user email (the token only not the full link)
        /// </summary>
        /// <param name="forgotPasswordRequestDto"> DTO to handle user data</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response if Email Sent succefully to the user.
        /// - Returns a 400 Bad Request response if the user data is invalid.
        /// </returns>
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
        /// <summary>
        /// Reset user password
        /// </summary>
        /// <param name="resetPasswordDto">DTO to handle user data</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response if password reset operatoin done succefully.
        /// - Returns a 400 Bad Request response if the user data is invalid.
        /// </returns>
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("invalid Request");
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
            return BadRequest("invalid Request");
        }

        /// <summary>
        /// end point to test the error logger
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 500 server error with the error id and add the error to the logs.
        /// </returns>
        /// <exception cref="Exception"> a custom exception to test the loggers</exception>
        [HttpPost]
        [Route("TestErrorLogger")]
        public Task<IActionResult> TestErrorLogger()
        {
            throw new Exception("this is a new exception to test");
        }
    }
}
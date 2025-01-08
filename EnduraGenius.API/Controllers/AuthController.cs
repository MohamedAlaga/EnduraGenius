using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.TokenRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(UserManager<User> userManager,ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
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
    }
}

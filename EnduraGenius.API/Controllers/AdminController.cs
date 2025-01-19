using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
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
                await _userManager.AddToRoleAsync(idetityUser, "User");
                await _userManager.AddToRoleAsync(idetityUser, "Admin");

                if (identityResult.Succeeded)
                {
                    return Ok(identityResult);
                }
            }

            return BadRequest("invalid user data :" + identityResult.Errors.ToList()[0].Description);
        }
    }
}

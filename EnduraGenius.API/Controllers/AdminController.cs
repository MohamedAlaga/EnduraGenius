using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Controllers
{
    /// <summary>
    /// Admin controller
    /// controller to manage admin acounts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Constructor for Admin controller
        /// </summary>
        /// <param name="userManager">user manger service</param>
        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Register new admin account [ADMIN ONLY]
        /// </summary>
        /// <param name="registerRequestDto">DTO contains user data</param>
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

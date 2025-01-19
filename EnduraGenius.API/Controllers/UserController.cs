using System.Security.Claims;
using AutoMapper;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.AuthRepository;
using EnduraGenius.API.Repositories.UserRepository;
using EnduraGenius.API.Repositories.UserWorkoutRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        public UserController(IUserRepository userRepository, IMapper mapper , IUserWorkoutRepository userWorkoutRepository, IAuthRepository authRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._userWorkoutRepository = userWorkoutRepository;
            this._authRepository = authRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] string? WorkoutsFilterOn, [FromQuery] string? WorkoutsFilterQuery, [FromQuery] int WorkoutsPageNumber = 1, [FromQuery] int WorkoutsPageSize = 20)
        {
            var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var user = await this._userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            var UserWorkouts = await this._userWorkoutRepository.GetUserWorkoutByUserId(userId, WorkoutsFilterOn, WorkoutsFilterQuery, WorkoutsPageNumber, WorkoutsPageSize);
            var userDTO = _mapper.Map<UserProfileResponseDTO>(user);
            userDTO.userWorkouts = _mapper.Map<List<UserWorkoutResponseDTO>>(UserWorkouts);
            return Ok(userDTO);
        }

        [HttpPut]
        [Route("Points")]
        public async Task<IActionResult> UpdateUserPoints([FromBody] EditPointsDTO newPoints )
        {
            var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var user = await this._userRepository.EditUserPoints(userId,newPoints.Points);
            if(user == null)
            {
                return NotFound();
            }
            newPoints.Points = user.Points;
            return Ok(newPoints);
        }

        [HttpPut]
        [Route("Points/Add")]
        public async Task<IActionResult> AddUserPoints([FromBody] EditPointsDTO AddPoints)
        {
            var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var user = await this._userRepository.AddUserPoints(userId, AddPoints.Points);
            if (user == null)
            {
                return NotFound();
            }
            AddPoints.Points = user.Points;
            return Ok(AddPoints);
        }

        [HttpPut]
        [Route("body")]
        public async Task<IActionResult> updateUserBody([FromBody] UpdateUserBodyDTO updateUserBodyDTO)
        {
            var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var user = await this._userRepository.EditUserBodyData(userId, updateUserBodyDTO.Weight, updateUserBodyDTO.Tall, updateUserBodyDTO.Age, updateUserBodyDTO.IsMale, updateUserBodyDTO.IsPublic);
            if (user == null)
            {
                return NotFound();
            }
            var userWorkouts = await this._userWorkoutRepository.GetUserWorkoutByUserId(userId, null, null, 1, 20);
            var userDTO = _mapper.Map<UserProfileResponseDTO>(user);
            userDTO.userWorkouts = _mapper.Map<List<UserWorkoutResponseDTO>>(userWorkouts);
            return Ok(_mapper.Map<UserProfileResponseDTO>(userDTO));
        }

        [HttpPut]
        [Route("UpdateUserPicture")]
        public async Task<IActionResult> UpdateUserPicture([FromForm] UpdateProfilePicRequestDTO picRequestDTO)
        {
            var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var user = await this._userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            if (picRequestDTO == null)
            {
                return BadRequest();
            }
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(picRequestDTO.newPicture.FileName)))
            {
                return BadRequest("unsupported file");
            }
            if (picRequestDTO.newPicture.Length > 10485760)
            {
                return BadRequest("file is too big");
            }
            var newPictureLink = await this._userRepository.UpdateUserPicture(userId, picRequestDTO.newPicture);
            if (newPictureLink == null)
            {
                return BadRequest();
            }
            return Ok(newPictureLink);
        }
    }
}

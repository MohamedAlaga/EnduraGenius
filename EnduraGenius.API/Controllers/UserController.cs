using AutoMapper;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.UserRepository;
using EnduraGenius.API.Repositories.UserWorkoutRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper , IUserWorkoutRepository userWorkoutRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._userWorkoutRepository = userWorkoutRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] string? WorkoutsFilterOn, [FromQuery] string? WorkoutsFilterQuery, [FromQuery] int WorkoutsPageNumber = 1, [FromQuery] int WorkoutsPageSize = 20)
        {
            var user = await this._userRepository.GetUserById("a4059c44-8a45-4200-bfa8-bd618696d3ea");
            if (user == null)
            {
                return NotFound();
            }
            var UserWorkouts = await this._userWorkoutRepository.GetUserWorkoutByUserId("a4059c44-8a45-4200-bfa8-bd618696d3ea", WorkoutsFilterOn, WorkoutsFilterQuery, WorkoutsPageNumber, WorkoutsPageSize);
            var userDTO = _mapper.Map<UserProfileResponseDTO>(user);
            userDTO.userWorkouts = _mapper.Map<List<UserWorkoutResponseDTO>>(UserWorkouts);
            return Ok(userDTO);
        }

        [HttpPut]
        [Route("Points")]
        public async Task<IActionResult> UpdateUserPoints([FromBody] EditPointsDTO newPoints )
        {
            var user = await this._userRepository.EditUserPoints("a4059c44-8a45-4200-bfa8-bd618696d3ea",newPoints.Points);
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
            var user = await this._userRepository.AddUserPoints("a4059c44-8a45-4200-bfa8-bd618696d3ea", AddPoints.Points);
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
            var user = await this._userRepository.EditUserBodyData("a4059c44-8a45-4200-bfa8-bd618696d3ea", updateUserBodyDTO.Weight, updateUserBodyDTO.Tall, updateUserBodyDTO.Age, updateUserBodyDTO.IsMale, updateUserBodyDTO.IsPublic);
            if (user == null)
            {
                return NotFound();
            }
            var userWorkouts = await this._userWorkoutRepository.GetUserWorkoutByUserId("a4059c44-8a45-4200-bfa8-bd618696d3ea", null, null, 1, 20);
            var userDTO = _mapper.Map<UserProfileResponseDTO>(user);
            userDTO.userWorkouts = _mapper.Map<List<UserWorkoutResponseDTO>>(userWorkouts);
            return Ok(_mapper.Map<UserProfileResponseDTO>(userDTO));
        }
    }
}

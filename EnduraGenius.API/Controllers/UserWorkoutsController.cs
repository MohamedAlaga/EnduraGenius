using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EnduraGenius.API.Repositories.UserWorkoutRepositories;
using EnduraGenius.API.Repositories.WorkoutsRepositories;
using EnduraGenius.API.Repositories.PlansUsersRepositories;
using EnduraGenius.API.Repositories.PlanWorkoutsRepositories;
using EnduraGenius.API.Models.DTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EnduraGenius.API.Repositories.AuthRepository;

namespace EnduraGenius.API.Controllers
{
    [Route("api/User/Workouts")]
    [ApiController]
    [Authorize]
    public class UserWorkoutsController : ControllerBase
    {
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
       

        public UserWorkoutsController(IUserWorkoutRepository userWorkoutRepository, IMapper mapper, IAuthRepository authRepository)
        {
            this._userWorkoutRepository = userWorkoutRepository;
            this._mapper = mapper;
            this._authRepository = authRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserWorkouts([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var userid = _authRepository.GetCurrentUserId();
            if (userid == null)
            {
                return Unauthorized();
            }
            var userWorkouts = await _userWorkoutRepository.GetUserWorkoutByUserId(userid,filterOn,filterQuery,pageNumber,pageSize);
            var workoutsDto = _mapper.Map<List<UserWorkoutResponseDTO>>(userWorkouts);
            return Ok(workoutsDto);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateUserWorkout([FromRoute] Guid id, [FromBody] UpdateUserWorkoutRequestDTO updateUserWorkoutRequestDTO)
        {
            var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var updatedUserWorkout = await _userWorkoutRepository.UpdateUserWorkout(id,userId, updateUserWorkoutRequestDTO.MaxWeight, updateUserWorkoutRequestDTO.LastWeight, updateUserWorkoutRequestDTO.TimesPerformed);
            if (updatedUserWorkout == null)
            {
                return BadRequest();
            }
            var finaluserWorkout = await _userWorkoutRepository.GetUserWorkoutByWorkoutId(userId, id);
            var mapper = _mapper.Map<UserWorkoutResponseDTO>(finaluserWorkout);
            return Ok(mapper);
        }
    }
}

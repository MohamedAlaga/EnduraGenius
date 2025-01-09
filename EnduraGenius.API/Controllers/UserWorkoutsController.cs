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

namespace EnduraGenius.API.Controllers
{
    [Route("api/User/Workouts")]
    [ApiController]
    [Authorize]
    public class UserWorkoutsController : ControllerBase
    {
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IPlansUsersRepository _plansUsersRepository;
        private readonly IPlanWorkoutsRepository _planWorkoutsRepository;
        private readonly IMapper _mapper;
        public UserWorkoutsController(IUserWorkoutRepository userWorkoutRepository, IWorkoutsRepository workoutsRepository, IPlansUsersRepository plansUsersRepository, IPlanWorkoutsRepository planWorkoutsRepository, IMapper mapper)
        {
            this._userWorkoutRepository = userWorkoutRepository;
            this._workoutsRepository = workoutsRepository;
            this._plansUsersRepository = plansUsersRepository;
            this._planWorkoutsRepository = planWorkoutsRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserWorkouts([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var userWorkout = await _userWorkoutRepository.GetUserWorkoutByWorkoutId(userId,id);
            if (userWorkout == null)
            {
                return NotFound();
            }
            var updatedUserWorkout = await _userWorkoutRepository.UpdateUserWorkout(userWorkout.Id, updateUserWorkoutRequestDTO.MaxWeight, updateUserWorkoutRequestDTO.LastWeight, updateUserWorkoutRequestDTO.TimesPerformed);
            if (updatedUserWorkout == null)
            {
                return BadRequest();
            }
            var finalworkout = await _userWorkoutRepository.GetUserWorkoutByWorkoutId(userId, updatedUserWorkout.Id);
            var mapper = _mapper.Map<UserWorkoutResponseDTO>(finalworkout);
            return Ok(mapper);
        }
    }
}

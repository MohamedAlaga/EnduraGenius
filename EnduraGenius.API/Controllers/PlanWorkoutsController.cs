using System.Security.Claims;
using AutoMapper;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.AuthRepository;
using EnduraGenius.API.Repositories.PlanRepositories;
using EnduraGenius.API.Repositories.PlanWorkoutsRepositories;
using EnduraGenius.API.Repositories.UserWorkoutRepositories;
using EnduraGenius.API.Repositories.WorkoutsRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Controllers
{
    [Route("api/Plan/Workouts")]
    [ApiController]
    [Authorize]
    public class PlanWorkoutsController : ControllerBase
    {
        private readonly IPlanWorkoutsRepository _planWorkoutsRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        public PlanWorkoutsController(IPlanWorkoutsRepository planWorkoutsRepository, IPlanRepository planRepository, IWorkoutsRepository _workoutsRepository, IMapper Imapper, IUserWorkoutRepository userWorkoutRepository, IAuthRepository authRepository)
        {
            this._planWorkoutsRepository = planWorkoutsRepository;
            this._planRepository = planRepository;
            this._workoutsRepository = _workoutsRepository;
            this._mapper = Imapper;
            this._userWorkoutRepository = userWorkoutRepository;
            this._authRepository = authRepository;
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePlanWorkout([FromRoute] Guid id, [FromBody] UpdatePlanWorkoutRequestDTO newWorkout)
        {
            var CurrentUserId = _authRepository.GetCurrentUserId();
            if (CurrentUserId == null)
            {
                return Unauthorized();
            }
            var isUpdated = await _planWorkoutsRepository.UpdatePlanWorkout(id,CurrentUserId, newWorkout.NewWorkoutId, newWorkout.NewReps, newWorkout.NewDayNumber, newWorkout.NewOrder);
            if (isUpdated == null)
            {
                return BadRequest();
            }
            var NewPlanWorkouts = await _planWorkoutsRepository.GetPlanWorkoutById(isUpdated.Id, CurrentUserId);
            var newPlanDto = _mapper.Map<PlanWorkoutsResponseDTO>(NewPlanWorkouts);
            return Ok(newPlanDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPlanWorkoutById([FromRoute] Guid id)
        {
            var CurrentUserId = _authRepository.GetCurrentUserId();
            if (CurrentUserId == null)
            {
                return Unauthorized();
            }
            var planWorkout = await _planWorkoutsRepository.GetPlanWorkoutById(id, CurrentUserId);
            if (planWorkout == null)
            {
                return NotFound();
            }
            var planWorkoutDto = _mapper.Map<PlanWorkoutsResponseDTO>(planWorkout);
            return Ok(planWorkoutDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlanWorkout([FromBody] CreatePlanWorkoutRequestDTO planWorkout)
        {
            var CurrentUserId = _authRepository.GetCurrentUserId();
            if (CurrentUserId == null)
            {
                return Unauthorized();
            }
            var plan = await _planRepository.GetPlanById(planWorkout.Plan,CurrentUserId);
            if (plan == null)
            {
                return BadRequest("incorrect plan id");
            }
            var workout = await _workoutsRepository.GetWorkoutById(planWorkout.Workout);
            if (workout == null)
            {
                return BadRequest("incorrect workout id");
            }
            var planWorkoutCreated = await _planWorkoutsRepository.CreatePlanWorkout(plan, workout, planWorkout.Reps, planWorkout.DayNumber, planWorkout.Order);
            if (planWorkoutCreated == null)
            {
                return BadRequest("incorrect data");
            }
            var userWorkout = await _userWorkoutRepository.CreateUserWorkout(workout, CurrentUserId);
            if (userWorkout == null) {
                return BadRequest("incorrect data");
            }
            var newPlanWorkout = await _planWorkoutsRepository.GetPlanWorkoutById(planWorkoutCreated.Id, CurrentUserId);
            var planWorkoutDto = _mapper.Map<PlanWorkoutsResponseDTO>(newPlanWorkout);
            return CreatedAtAction(nameof(GetPlanWorkoutById), new { id = planWorkoutCreated.Id }, planWorkoutDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePlanWorkout([FromRoute] Guid id)
        {
            var CurrentUserId = _authRepository.GetCurrentUserId();
            if (CurrentUserId == null)
            {
                return Unauthorized();
            }
            var isDeleted = await _planWorkoutsRepository.DeletePlanWorkout(id,CurrentUserId);
            if (isDeleted == false)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

using EnduraGenius.API.Repositories.PlanRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EnduraGenius.API.Data;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Repositories.PlanWorkoutsRepositories;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.UserWorkoutRepositories;
using EnduraGenius.API.Repositories.WorkoutsRepositories;
using EnduraGenius.API.Repositories.PlansUsersRepositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EnduraGenius.API.Repositories.AuthRepository;
namespace EnduraGenius.API.Controllers
{
    /// <summary>
    /// Plans Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlansController : ControllerBase
    {
        private readonly IPlanRepository _plansRepository;
        private readonly IMapper _mapper;
        private readonly IPlanWorkoutsRepository _planWorkoutsRepository;
        private readonly IPlansUsersRepository _plansUsersRepository;
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IAuthRepository _authRepository;
        /// <summary>
        ///  constractor for PlansController
        /// </summary>
        public PlansController( IPlanRepository plansRepository, IMapper mapper , IPlanWorkoutsRepository planWorkoutsRepository, IUserWorkoutRepository _userWorkoutRepository, IWorkoutsRepository workoutsRepository, IPlansUsersRepository plansUsersRepository, IAuthRepository authRepository)
        {
            this._plansRepository = plansRepository;
            this._mapper = mapper;
            this._planWorkoutsRepository = planWorkoutsRepository;
            this._userWorkoutRepository = _userWorkoutRepository;
            this._workoutsRepository = workoutsRepository;
            this._plansUsersRepository = plansUsersRepository;
            this._authRepository = authRepository;
        }
        /// <summary>
        /// Get all available plans for the user .
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response All availbale plans .
        /// - Returns a 401 Unauthorized response if the user not found.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllplans()
        {
            var CurrentUserId = _authRepository.GetCurrentUserId();
            if (CurrentUserId == null)
            {
                return Unauthorized();
            }
            var plans = await _plansRepository.GetPublicPlans(CurrentUserId);
            var plansDto = new List<PlanResponseDTO>();
            foreach (var plan in plans)
            {
                var PlansWorkouts = await _planWorkoutsRepository.GetPlanWorkoutByPlanId(plan.Id,CurrentUserId);
                var planDto = _mapper.Map<PlanResponseDTO>(plan);
                planDto.workouts = _mapper.Map<List<PlanWorkoutsResponseDTO>>(PlansWorkouts);
                plansDto.Add(planDto);
            }
            return Ok(plansDto);
        }

        /// <summary>
        /// Get a plan by id
        /// </summary>
        /// <param name="id">the id of the requested plan</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains the requested plan .
        /// - Returns a 401 Unauthorized response if the user not found.
        /// - Returns a 404 Not Found response if the plan not found.
        /// </returns>
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPlanById([FromRoute] Guid id)
        {
            var CurrentUserId = _authRepository.GetCurrentUserId();
            if (CurrentUserId == null)
            {
                return Unauthorized();
            }
            var plan = await _plansRepository.GetPlanById(id, CurrentUserId);
            if (plan == null)
            {
                return NotFound();
            }
            var PlansWorkouts = await _planWorkoutsRepository.GetPlanWorkoutByPlanId(plan.Id, CurrentUserId);
            var planDto = _mapper.Map<PlanResponseDTO>(plan);
            planDto.workouts = _mapper.Map<List<PlanWorkoutsResponseDTO>>(PlansWorkouts);
            return Ok(planDto);
        }

        /// <summary>
        /// Create a new plan
        /// </summary>
        /// <param name="plan">DTO contains all required data for the plan</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 201 CreatedAtAction response contains the new plan .
        /// - Returns a 401 Unauthorized response if the user not found.
        /// - Returns a 400 Bad Request if the data is not correct.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] CreatePlanRequestDTO plan)
        {
            var CurrentUserId = _authRepository.GetCurrentUserId();
            if (CurrentUserId == null)
            {
                return Unauthorized();
            }
            var newplan = await _plansRepository.CreatePlan(plan.Name,plan.Descreption, CurrentUserId, false);
            if (newplan == null)
            {
                return BadRequest();
            }
            foreach (var planworkout in plan.workoutsDtos)
            {
                var workout = await _workoutsRepository.GetWorkoutById(planworkout.WorkoutId);
                if (workout == null)
                {
                    return BadRequest();
                }
                var WorkoutDTO = _mapper.Map<GetWorkoutDto>(workout);
                var newPlanWorkout = await _planWorkoutsRepository.CreatePlanWorkout(newplan, workout, planworkout.Reps, planworkout.DayNumber, planworkout.Order);
                if (newPlanWorkout == null)
                {
                    return BadRequest();
                }
                await _userWorkoutRepository.CreateUserWorkout(workout, CurrentUserId);
            }
            await _plansUsersRepository.CreatePlanUser(newplan, CurrentUserId);
            var PlansWorkouts = await _planWorkoutsRepository.GetPlanWorkoutByPlanId(newplan.Id,CurrentUserId);
            var planDto = _mapper.Map<PlanResponseDTO>(newplan);
            planDto.workouts = _mapper.Map<List<PlanWorkoutsResponseDTO>>(PlansWorkouts);

            return CreatedAtAction(nameof(GetPlanById), new { id = newplan.Id },planDto);
        }

        /// <summary>
        /// Update a plan
        /// </summary>
        /// <param name="id">the requested plan id</param>
        /// <param name="plan">the new plan data</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response if updated succefully.
        /// - Returns a 401 Unauthorized response if the user not found.
        /// - Returns a 404 Not Found response if the plan not found.
        /// </returns>
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePlan([FromRoute] Guid id, [FromBody] UpdatePlanRequestDTO plan)
        {
            var CurrentUserId = _authRepository.GetCurrentUserId();
            if (CurrentUserId == null)
            {
                return Unauthorized();
            }
            var planToUpdate = await _plansRepository.GetPlanById(id,CurrentUserId);
            if (planToUpdate == null)
            {
                return NotFound();
            }
            var updatedPlan = await _plansRepository.UpdatePlan(id,CurrentUserId, plan.PlanName, plan.PlanDescription, plan.IsPublic,plan.workoutsDtos);
            if (updatedPlan == null)
            {
                return BadRequest();
            }
            var PlansWorkouts = await _planWorkoutsRepository.GetPlanWorkoutByPlanId(updatedPlan.Id, CurrentUserId);
            var planDto = _mapper.Map<PlanResponseDTO>(updatedPlan);
            planDto.workouts = _mapper.Map<List<PlanWorkoutsResponseDTO>>(PlansWorkouts);
            return Ok(planDto);
        }

        /// <summary>
        /// create a custom prosplit plan based on current user data
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains the plan data.
        /// - Returns a 401 Unauthorized response if the user not found.
        /// </returns>
        [HttpGet]
        [Route("custom/prosplit")]
        public async Task<IActionResult> GetCustomProsplit()
        {
            var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var workouts = await _plansRepository.CreateProSplitWokoutPlan(userId);
            return Ok(workouts);
        }


        /// <summary>
        /// create a custom upper lower plan based on current user data
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains the plan data.
        /// - Returns a 401 Unauthorized response if the user not found.
        /// </returns>
        [HttpGet]
        [Route("custom/UpperLower")]
        public async Task<IActionResult> GetCustomUpperLower()
        {
            var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var workouts = await _plansRepository.CreateUpperLowerPlan(userId);
            return Ok(workouts);
        }
    }
}

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
        // GET: api/Plans
        // get all plans from the database
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
        // GET: api/Plans/{id}
        // get plan by id
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

        // POST: api/Plans
        // create a [plan]
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


        // PUT: api/Plans/{id}
        // update a plan
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

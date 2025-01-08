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
namespace EnduraGenius.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IPlanRepository _plansRepository;
        private readonly IMapper _mapper;
        private readonly IPlanWorkoutsRepository _planWorkoutsRepository;
        private readonly IPlansUsersRepository _plansUsersRepository;
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IWorkoutsRepository _workoutsRepository;
        public PlansController( IPlanRepository plansRepository, IMapper mapper , IPlanWorkoutsRepository planWorkoutsRepository, IUserWorkoutRepository _userWorkoutRepository, IWorkoutsRepository workoutsRepository, IPlansUsersRepository plansUsersRepository)
        {
            this._plansRepository = plansRepository;
            this._mapper = mapper;
            this._planWorkoutsRepository = planWorkoutsRepository;
            this._userWorkoutRepository = _userWorkoutRepository;
            this._workoutsRepository = workoutsRepository;
            this._plansUsersRepository = plansUsersRepository;
        }
        // GET: api/Plans
        // get all plans from the database
        [HttpGet]
        public async Task<IActionResult> GetAllplans()
        {
            var plans = await _plansRepository.GetPublicPlans();
            var plansDto = new List<PlanResponseDTO>();
            foreach (var plan in plans)
            {
                var PlansWorkouts = await _planWorkoutsRepository.GetPlanWorkoutByPlanId(plan.Id);
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
            var plan = await _plansRepository.GetPlanById(id);
            if (plan == null)
            {
                return NotFound();
            }
            var PlansWorkouts = await _planWorkoutsRepository.GetPlanWorkoutByPlanId(plan.Id);
            var planDto = _mapper.Map<PlanResponseDTO>(plan);
            planDto.workouts = _mapper.Map<List<PlanWorkoutsResponseDTO>>(PlansWorkouts);
            return Ok(planDto);
        }

        // POST: api/Plans
        // create a [plan]
        [HttpPost]
        public async Task<IActionResult> CreatePlan([FromBody] CreatePlanRequestDTO plan)
        {
            var CurrentUserId = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
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
            var PlansWorkouts = await _planWorkoutsRepository.GetPlanWorkoutByPlanId(newplan.Id);
            var planDto = _mapper.Map<PlanResponseDTO>(newplan);
            planDto.workouts = _mapper.Map<List<PlanWorkoutsResponseDTO>>(PlansWorkouts);

            return CreatedAtAction(nameof(GetPlanById), new { id = newplan.Id },planDto);
        }

    }
}

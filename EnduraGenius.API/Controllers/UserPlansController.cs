using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EnduraGenius.API.Repositories.PlansUsersRepositories;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Repositories.PlanRepositories;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.PlanWorkoutsRepositories;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EnduraGenius.API.Repositories.AuthRepository;

namespace EnduraGenius.API.Controllers
{
    [Route("api/Plans/User/")]
    [ApiController]
    [Authorize]
    public class UserPlansController : ControllerBase
    {
        private readonly IPlansUsersRepository _plansUsersRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        private readonly IPlanWorkoutsRepository _planWorkoutsRepository;
        private readonly IAuthRepository _authRepository;
        public UserPlansController(IPlansUsersRepository plansUsersRepository, IMapper mapper, IPlanRepository plan, IPlanWorkoutsRepository planWorkoutsRepository, IAuthRepository authRepository)
        {
            this._plansUsersRepository = plansUsersRepository;
            this._mapper = mapper;
            this._planRepository = plan;
            this._planWorkoutsRepository = planWorkoutsRepository;
            this._authRepository = authRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserPlans()
        {
            var CurrentUserId = _authRepository.GetCurrentUserId();
            if (CurrentUserId == null)
            {
                return Unauthorized();
            }
            var userPlans = await _plansUsersRepository.GetPlansUserByUserId(CurrentUserId);
            var AllPlans = new List<Plan>();
            foreach (var userPlan in userPlans)
            {
                var plan = await _planRepository.GetPlanById(userPlan.PlanId, CurrentUserId);
                if (plan != null)
                {
                    AllPlans.Add(plan);
                }
            }
            var plansDto = new List<PlanResponseDTO>();
            foreach (var plan in AllPlans)
            {
                var PlansWorkouts = await _planWorkoutsRepository.GetPlanWorkoutByPlanId(plan.Id,CurrentUserId);
                var planDto = _mapper.Map<PlanResponseDTO>(plan);
                planDto.workouts = _mapper.Map<List<PlanWorkoutsResponseDTO>>(PlansWorkouts);
                plansDto.Add(planDto);
            }
            return Ok(plansDto);
        }
        [HttpGet]
        [Route("Current")]
        public async Task<IActionResult> GetCurrentPlan()
        {
            var userid = _authRepository.GetCurrentUserId();
            if (userid == null)
            {
                return Unauthorized();
            }
            var userCurrentPlan = await _plansUsersRepository.GetUserCurrentPlan(userid);
            if (userCurrentPlan == null)
            {
                return NotFound();
            }
            var plan = await _planRepository.GetPlanById(userCurrentPlan.PlanId,userid);
            if (userCurrentPlan == null)
            {
                return NotFound();
            }
            var PlansWorkouts = await _planWorkoutsRepository.GetPlanWorkoutByPlanId(plan.Id,userid);
            var planDto = _mapper.Map<PlanResponseDTO>(plan);
            planDto.workouts = _mapper.Map<List<PlanWorkoutsResponseDTO>>(PlansWorkouts);
            return Ok(planDto);
        }
        [HttpPut]
        [Route("Current/{PlanId:Guid}")]
        public async Task<IActionResult> SetCurrentPlan([FromRoute] Guid PlanId)
        {
            var userid = _authRepository.GetCurrentUserId();
            if (userid == null)
            {
                return Unauthorized();
            }
            var userCurrentPlan = await _plansUsersRepository.SetCurrentPlan(PlanId, userid);
            if (userCurrentPlan == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Unsubscribe/{PlanId:Guid}")]
        public async Task<IActionResult> UnsubscribeFromPlan([FromRoute] Guid PlanId)
        {
            var userid = _authRepository.GetCurrentUserId();
            if (userid == null)
            {
                return Unauthorized();
            }
            var result = await _plansUsersRepository.UnsubscibeUserFromPlan(userid, PlanId);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}

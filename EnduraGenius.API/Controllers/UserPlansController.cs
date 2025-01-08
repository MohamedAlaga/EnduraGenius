﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EnduraGenius.API.Repositories.PlansUsersRepositories;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Repositories.PlanRepositories;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.PlanWorkoutsRepositories;
using System.Numerics;

namespace EnduraGenius.API.Controllers
{
    [Route("api/Plans/User/")]
    [ApiController]
    public class UserPlansController : ControllerBase
    {
        private readonly IPlansUsersRepository _plansUsersRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        private readonly IPlanWorkoutsRepository _planWorkoutsRepository;
        public UserPlansController(IPlansUsersRepository plansUsersRepository, IMapper mapper, IPlanRepository plan, IPlanWorkoutsRepository planWorkoutsRepository)
        {
            this._plansUsersRepository = plansUsersRepository;
            this._mapper = mapper;
            this._planRepository = plan;
            this._planWorkoutsRepository = planWorkoutsRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserPlans()
        {
            var userid = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            var userPlans = await _plansUsersRepository.GetPlansUserByUserId(userid);
            var AllPlans = new List<Plan>();
            foreach (var userPlan in userPlans)
            {
                var plan = await _planRepository.GetPlanById(userPlan.PlanId);
                if (plan != null)
                {
                    AllPlans.Add(plan);
                }
            }
            var plansDto = new List<PlanResponseDTO>();
            foreach (var plan in AllPlans)
            {
                var PlansWorkouts = await _planWorkoutsRepository.GetPlanWorkoutByPlanId(plan.Id);
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
            var userid = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            var userCurrentPlan = await _plansUsersRepository.GetUserCurrentPlan(userid);
            if (userCurrentPlan == null)
            {
                return NotFound();
            }
            var plan = await _planRepository.GetPlanById(userCurrentPlan.PlanId);
            if (userCurrentPlan == null)
            {
                return NotFound();
            }
            var PlansWorkouts = await _planWorkoutsRepository.GetPlanWorkoutByPlanId(plan.Id);
            var planDto = _mapper.Map<PlanResponseDTO>(plan);
            planDto.workouts = _mapper.Map<List<PlanWorkoutsResponseDTO>>(PlansWorkouts);
            return Ok(planDto);
        }
        [HttpPut]
        [Route("Current/{PlanId:Guid}")]
        public async Task<IActionResult> SetCurrentPlan([FromRoute] Guid PlanId)
        {
            var userid = "a4059c44-8a45-4200-bfa8-bd618696d3ea";
            var userCurrentPlan = await _plansUsersRepository.SetCurrentPlan(PlanId, userid);
            if (userCurrentPlan == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

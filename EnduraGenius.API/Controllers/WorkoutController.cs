using EnduraGenius.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using EnduraGenius.API.Repositories.MuscleRepositories;
using EnduraGenius.API.Repositories.WorkoutsRepositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace EnduraGenius.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class WorkoutController : ControllerBase
    {
        private readonly EnduraGeniusDBContext _dbcontext;
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IMapper _mapper;
        private readonly IMuscleRepository _muscleRepository;
        public WorkoutController(EnduraGeniusDBContext dbcontext, IWorkoutsRepository workoutsRepository, IMapper mapper, IMuscleRepository muscleRepository)
        {
            this._dbcontext = dbcontext;
            this._workoutsRepository = workoutsRepository;
            this._mapper = mapper;
            this._muscleRepository = muscleRepository;
        }

        // GET: api/Workout/{id}
        // Get workout by id
        // done
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWorkoutById([FromRoute] Guid id)
        {
            var workout = await _workoutsRepository.GetWorkoutById(id);
            if (workout == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetWorkoutDto>(workout));
        }

        // GET: api/Workout
        // Get all workouts
        // done
        [HttpGet]
        public async Task<IActionResult> GetWorkouts([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var workouts = await _workoutsRepository.GetWorkouts(filterOn, filterQuery, pageNumber, pageSize, true);
            return Ok(_mapper.Map<List<GetWorkoutDto>>(workouts));
        }

        // POST: api/Workout
        // Create a new workout
        // done
        [HttpPost]
        public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutRequestDTO createWorkoutRequestDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var mainMuscle = await _muscleRepository.GetMuscleByName(createWorkoutRequestDTO.MainMuscleName);
            if (mainMuscle == null)
            {
                return NotFound("Main muscle name not found");
            }
            var secondaryMuscle = await _muscleRepository.GetMuscleByName(createWorkoutRequestDTO.SecondaryMuscleName);
            if (secondaryMuscle == null)
            {
                return NotFound("Secondary muscle name not found");
            }
            var workout = _mapper.Map<Workout>(createWorkoutRequestDTO);
            workout.IsCertified = false;
            workout = await _workoutsRepository.CreateWorkout(workout, mainMuscle, secondaryMuscle, userId);
            if (workout == null)
            {
                return BadRequest();
            }
            var workoutDto = _mapper.Map<CreateWorkoutRequestDTO>(workout);
            return CreatedAtAction(nameof(GetWorkoutById), new { id = workout.Id }, workoutDto);
        }

        // PUT: api/Workout/{id}
        // Update a workout
        // done
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWorkout([FromRoute] Guid id, [FromBody] GetWorkoutDto updateWorkoutDto)
        {
            var workout = await _workoutsRepository.GetWorkoutById(id);
            if (workout == null)
            {
                return NotFound();
            }
            var isUpdated = await _workoutsRepository.UpdateWorkout(workout, updateWorkoutDto);
            if (isUpdated)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Workout/{id}
        // Delete a workout
        // done
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWorkout([FromRoute] Guid id)
        {
            var workout = await _workoutsRepository.DeleteWorkout(id);
            if (workout == false)
            {
                return NotFound();
            }
            return Ok();
        }
        // GET: api/Workout/uncertified
        // Get all uncertified workouts
        // done
        [HttpGet]
        [Route("uncertified")]
        public async Task<IActionResult> GetUncertifiedWorkouts([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var workouts = await _workoutsRepository.GetWorkouts(filterOn, filterQuery, pageNumber, pageSize, false);
            return Ok(_mapper.Map<List<GetWorkoutDto>>(workouts));
        }

        // PUT: api/Workout/certify/{id}
        // Change certification status of a workout
        // done
        [HttpPut]
        [Route("certify/{id:Guid}")]
        public async Task<IActionResult> ChangeCertificationStatus([FromRoute] Guid id)
        {
            var workout = await _workoutsRepository.ChangeCertificationStatus(id);
            if (workout == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

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
using EnduraGenius.API.Repositories.AuthRepository;
namespace EnduraGenius.API.Controllers
{
    /// <summary>
    /// Workout Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutsRepository _workoutsRepository;
        private readonly IMapper _mapper;
        private readonly IMuscleRepository _muscleRepository;
        private readonly IAuthRepository _authRepository;
        /// <summary>
        /// Constructor for WorkoutController
        /// </summary>
        public WorkoutController(IWorkoutsRepository workoutsRepository, IMapper mapper, IMuscleRepository muscleRepository, IAuthRepository authRepository)
        {
            this._workoutsRepository = workoutsRepository;
            this._mapper = mapper;
            this._muscleRepository = muscleRepository;
            this._authRepository = authRepository;
        }

        /// <summary>
        /// Get a workout by id
        /// </summary>
        /// <param name="id">id of the workout</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains the workout.
        /// - Returns a 404 NotFound response if the workout not found.
        /// </returns>
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

        /// <summary>
        ///  Get all certfied workouts
        /// </summary>
        /// <param name="filterOn">name of the param to filter results based on</param>
        /// <param name="filterQuery">search query</param>
        /// <param name="pageNumber">the number of page needed</param>
        /// <param name="pageSize">the size of the page</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains All the workout.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetWorkouts([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var workouts = await _workoutsRepository.GetWorkouts(filterOn, filterQuery, pageNumber, pageSize, true);
            return Ok(_mapper.Map<List<GetWorkoutDto>>(workouts));
        }

        /// <summary>
        /// Create a new workout
        /// </summary>
        /// <param name="createWorkoutRequestDTO">DTO contains new Workout data</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 201 CreatedAtAction response contains the workout.
        /// - Returns a 400 BadRequest response if data is not valid.
        /// - Returns a 401 Unauthorized response if the user not found.
        /// - Returns a 404 NotFound response if the muscles are not found.
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutRequestDTO createWorkoutRequestDTO)
        {
            var userId = _authRepository.GetCurrentUserId();
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

        /// <summary>
        /// Update a workout
        /// </summary>
        /// <param name="id">workout to be updated</param>
        /// <param name="updateWorkoutDto">DTO contains the new workout data</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response.
        /// - Returns a 400 BadRequest response if data is not valid.
        /// - Returns a 404 NotFound response if the workout not found.
        /// </returns>
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "admin")]
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

        /// <summary>
        /// Delete a workout
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response if deleted succffuly.
        /// - Returns a 404 NotFound response if the workout not found.
        /// </returns>
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteWorkout([FromRoute] Guid id)
        {
            var workout = await _workoutsRepository.DeleteWorkout(id);
            if (workout == false)
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Get all uncertified workouts
        /// </summary>
        /// <param name="filterOn">name of the param to filter results based on</param>
        /// <param name="filterQuery">search query</param>
        /// <param name="pageNumber">the number of page needed</param>
        /// <param name="pageSize">the size of the page</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response list of all un certfied workouts.
        /// </returns>
        [HttpGet]
        [Route("uncertified")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUncertifiedWorkouts([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var workouts = await _workoutsRepository.GetWorkouts(filterOn, filterQuery, pageNumber, pageSize, false);
            return Ok(_mapper.Map<List<GetWorkoutDto>>(workouts));
        }

        /// <summary>
        /// Change the certification status of a workout
        /// </summary>
        /// <param name="id">the id of the workout</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response if updated succffuly.
        /// - Returns a 404 NotFound response if the workout not found.
        /// </returns>
        [HttpPut]
        [Route("certify/{id:Guid}")]
        [Authorize(Roles = "admin")]
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

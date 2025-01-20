using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using EnduraGenius.API.Repositories.MuscleRepositories;
using Microsoft.AspNetCore.Authorization;

namespace EnduraGenius.API.Controllers
{
    /// <summary>
    /// Muscles Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MusclesController : ControllerBase
    {
        private readonly IMuscleRepository _muscleRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor for MusclesController
        /// </summary>
        public MusclesController(IMuscleRepository muscleRepository, IMapper mapper)
        {
            _muscleRepository = muscleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all muscles
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains all muscles.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetMuscles()
        {
            var muscles = await _muscleRepository.GetMuscles();
            return Ok(muscles);
        }

        /// <summary>
        /// Get muscle by id
        /// </summary>
        /// <param name="id"> id of the needed muscle</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains only needed muscle.
        /// - Returns a 404 Not Found response if the muscle not found.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMuscleById([FromRoute] Guid id)
        {
            var muscle = await _muscleRepository.GetMuscleById(id);
            if (muscle == null)
            {
                return NotFound();
            }
            return Ok(muscle);
        }

        /// <summary>
        /// create a new muscle
        /// </summary>
        /// <param name="muscleForCreationDto">DTO contains muscle data</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 201 CreatedAtAction response contains new muscle.
        /// - Returns a 400 bad request response if the muscle data is not correct.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateMuscle([FromBody] CreateMuscleDTO muscleForCreationDto)
        {
            var muscle = _mapper.Map<Muscle>(muscleForCreationDto);
            var NewMuscle = await _muscleRepository.CreateMuscle(muscle);
            if (NewMuscle == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetMuscleById), new { id = NewMuscle.Id }, muscleForCreationDto);
        }

        /// <summary>
        /// Update an existing muscle
        /// </summary>
        /// <param name="id">the id of the muscle to update</param>
        /// <param name="muscleForUpdateDto">DTO contains new Muscle Data</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains new muscle if updated succefully.
        /// - Returns a 404 Not Found response if the muscle not found.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMuscle([FromRoute] Guid id,[FromBody] UpdateMuscleDto muscleForUpdateDto)
        {
            var oldMuscle = await _muscleRepository.GetMuscleById(id);
            if (oldMuscle == null)
            {
                return NotFound();
            }
            var Muscle = await _muscleRepository.UpdateMuscle(oldMuscle, muscleForUpdateDto);
            return Ok(Muscle);
        }

        /// <summary>
        /// Delete an existing muscle
        /// </summary>
        /// <param name="id">the id of the muscle</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains new muscle if deleted succefully.
        /// - Returns a 404 Not Found response if the muscle not found.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMuscle([FromRoute] Guid id)
        {
            var isDeleted = await _muscleRepository.DeleteMuscle(id);
            if (isDeleted == false)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

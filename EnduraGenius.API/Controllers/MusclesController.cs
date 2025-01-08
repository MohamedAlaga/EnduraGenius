using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using EnduraGenius.API.Repositories.MuscleRepositories;

namespace EnduraGenius.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusclesController : ControllerBase
    {
        private readonly IMuscleRepository _muscleRepository;
        private readonly IMapper _mapper;
        public MusclesController(IMuscleRepository muscleRepository, IMapper mapper)
        {
            _muscleRepository = muscleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMuscles()
        {
            var muscles = await _muscleRepository.GetMuscles();
            return Ok(muscles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMuscleById([FromRoute] Guid id)
        {
            var muscle = await _muscleRepository.GetMuscleById(id);
            return Ok(muscle);
        }

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

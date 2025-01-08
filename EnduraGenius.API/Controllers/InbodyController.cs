using AutoMapper;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.InbodyRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InbodyController : ControllerBase
    {
        private readonly IInbodyRepository _inbodyRepository;
        private readonly IMapper _mapper; 
        public InbodyController(IMapper mapper, IInbodyRepository inbodyRepository)
        {
            this._inbodyRepository = inbodyRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetInbody()
        {
           var inbody = await this._inbodyRepository.GetInbodyByUserId("a4059c44-8a45-4200-bfa8-bd618696d3ea");
           var inbodyDTO = _mapper.Map<List<InbodyResponseDTO>>(inbody);
           return Ok(inbodyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostInbody([FromBody] RequestInbodyDTO requestInbodyDTO)
        {
            string userId = "a4059c44-8a45-4200-bfa8-bd618696d3ea"; 
            var inbody = await _inbodyRepository.InsertInbodyAsync(userId, requestInbodyDTO.ActivityLevel, requestInbodyDTO.name);
            if (inbody == null)
            {
                return BadRequest();
            }
            var inbodyDTO = _mapper.Map<InbodyResponseDTO>(inbody);
            return CreatedAtAction(nameof(GetInbodyById), new { id = inbody.Id },inbodyDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetInbodyById([FromRoute]Guid id)
        {
            var inbody = await this._inbodyRepository.GetInbodyAsync(id,"a4059c44-8a45-4200-bfa8-bd618696d3ea");
            if (inbody == null)
            {
                return NotFound();
            }
            var inbodyDTO = _mapper.Map<InbodyResponseDTO>(inbody);
            return Ok(inbodyDTO);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteInbody(Guid id)
        {
            var result = await this._inbodyRepository.DeleteInbody(id, "a4059c44-8a45-4200-bfa8-bd618696d3ea");
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}

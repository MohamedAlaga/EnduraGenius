using AutoMapper;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.AuthRepository;
using EnduraGenius.API.Repositories.InbodyRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Controllers
{
    /// <summary>
    /// controller to handle user inbody data
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InbodyController : ControllerBase
    {
        private readonly IInbodyRepository _inbodyRepository;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        /// <summary>
        /// Constructor for InbodyController
        /// </summary>
        /// <param name="mapper"> mapping service</param>
        /// <param name="inbodyRepository"> repository to fetch all inbody data</param>
        /// <param name="authRepository"> repository to get auth data (current user id/roles)</param>
        public InbodyController(IMapper mapper, IInbodyRepository inbodyRepository, IAuthRepository authRepository)
        {
            this._inbodyRepository = inbodyRepository;
            this._mapper = mapper;
            this._authRepository = authRepository;
        }

        /// <summary>
        /// Get all inbody data for the current user
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains all previous inbodies if user is found .
        /// - Returns a 401 Bad Request response if the user not found.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetInbody()
        {
           var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var inbody = await this._inbodyRepository.GetInbodyByUserId(userId);
           var inbodyDTO = _mapper.Map<List<InbodyResponseDTO>>(inbody);
           return Ok(inbodyDTO);
        }

        /// <summary>
        /// Post new inbody data for the current user
        /// </summary>
        /// <param name="requestInbodyDTO">DTO to handle required data for the inbody</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 201 CreatedAtACtion response conains the new inbody and it's location in the header .
        /// - Returns a 401 Bad Request response if the user not found.
        /// - Returns a 400 Bad Request response if the inbody data is invalid.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PostInbody([FromBody] RequestInbodyDTO requestInbodyDTO)
        {
            var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var inbody = await _inbodyRepository.InsertInbodyAsync(userId, requestInbodyDTO.ActivityLevel, requestInbodyDTO.name);
            if (inbody == null)
            {
                return BadRequest();
            }
            var inbodyDTO = _mapper.Map<InbodyResponseDTO>(inbody);
            return CreatedAtAction(nameof(GetInbodyById), new { id = inbody.Id },inbodyDTO);
        }

        /// <summary>
        /// get one inbody data for the current user
        /// </summary>
        /// <param name="id"> the id of the needed inbody</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response contains requested inbody .
        /// - Returns a 401 Bad Request response if the user not found.
        /// - Returns a 404 Bad Request response if the inbody id does not exist.
        /// </returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetInbodyById([FromRoute]Guid id)
        {
            var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var inbody = await this._inbodyRepository.GetInbodyAsync(id,userId);
            if (inbody == null)
            {
                return NotFound();
            }
            var inbodyDTO = _mapper.Map<InbodyResponseDTO>(inbody);
            return Ok(inbodyDTO);
        }
        /// <summary>
        /// Delete one inbody data for the current user
        /// </summary>
        /// <param name="id">the id of the inbody</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// - Returns a 200 OK response if the inbody deleted succefully.
        /// - Returns a 401 Bad Request response if the user not found.
        /// - Returns a 404 Bad Request response if the inbody id does not exist.
        /// </returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteInbody(Guid id)
        {
            var userId = _authRepository.GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var result = await this._inbodyRepository.DeleteInbody(id,userId);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}

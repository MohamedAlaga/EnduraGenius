﻿using System.Security.Claims;
using AutoMapper;
using EnduraGenius.API.Models.Domain;
using EnduraGenius.API.Models.DTO;
using EnduraGenius.API.Repositories.InbodyRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnduraGenius.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var inbody = await this._inbodyRepository.GetInbodyByUserId(userId);
           var inbodyDTO = _mapper.Map<List<InbodyResponseDTO>>(inbody);
           return Ok(inbodyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostInbody([FromBody] RequestInbodyDTO requestInbodyDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetInbodyById([FromRoute]Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteInbody(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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

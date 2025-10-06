using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaSaudeFeminina.DTOs;
using MinhaSaudeFeminina.DTOs.Profiles;
using MinhaSaudeFeminina.Models.UserProfile;
using MinhaSaudeFeminina.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MinhaSaudeFeminina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _service;

        public ProfileController(ProfileService service)
        {
            _service = service;
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profile = await _service.GetAsync(id);
            if (profile == null)
                return NotFound(new { Message = "Perfil não encontrado." });

            var userId = int.Parse(User.FindFirst("id")!.Value);
            if (!User.IsInRole("Admin") && profile.UserId != userId)
                return Forbid();

            return Ok(profile);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var profiles = await _service.GetAllAsync();
            if (profiles == null)
                return NotFound(new { Message = "Nenhum perfil encontrado." });

            return Ok(profiles);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] ProfileRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst("id")!.Value);

            var created = await _service.CreateAsync(dto, userId);

            return CreatedAtAction(nameof(Get), new { id = created.ProfileId }, created);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProfileRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profile = await _service.GetAsync(id);
            if (profile == null)
                return NotFound(new { Message = "Perfil não encontrado." });

            var userId = int.Parse(User.FindFirst("id")!.Value);
            if (!User.IsInRole("Admin") && profile.UserId != userId)
                return Forbid();

            var updated = await _service.UpdateAsync(id, dto);

            return Ok(updated);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profile = await _service.GetAsync(id);
            if (profile == null)
                return NotFound(new { Message = "Perfil não encontrado." });

            var userId = int.Parse(User.FindFirst("id")!.Value);
            if (!User.IsInRole("Admin") && profile.UserId != userId)
                return Forbid();

            var delete = await _service.DeleteAsync(id);
            if (!delete)
                return NotFound(new { Message = "Perfil não encontrado." });

            return Ok("Perfil deletado");
        }
    }
}

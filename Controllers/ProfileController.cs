using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaSaudeFeminina.DTOs;
using MinhaSaudeFeminina.DTOs.Profiles;
using MinhaSaudeFeminina.Models.UserProfile;
using MinhaSaudeFeminina.Services;
using System.Security.Claims;
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
            var profile = await _service.GetAsync(id);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (!User.IsInRole("Admin") && profile.Data!.UserId != userId)
                return Forbid();

            return Ok(profile);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllDtosAsync());

        [Authorize(Roles = "User, Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] ProfileRegisterDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var created = await _service.CreateAsync(dto, userId);

            return CreatedAtAction(nameof(Get), new { id = created.Data!.ProfileId }, created);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProfileRegisterDto dto)
        {
            var profile = await _service.GetAsync(id);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (!User.IsInRole("Admin") && profile.Data!.UserId != userId)
                return Forbid();

            return Ok(await _service.UpdateAsync(id, dto));
        }

        [Authorize(Roles = "User, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _service.GetAsync(id);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (!User.IsInRole("Admin") && profile.Data!.UserId != userId)
                return Forbid();

            return Ok(await _service.RemoveAsync(id));
        }
    }
}

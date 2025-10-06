using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaSaudeFeminina.DTOs;
using MinhaSaudeFeminina.DTOs.Gender;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Services;
using System.Runtime.CompilerServices;

namespace MinhaSaudeFeminina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenderController : ControllerBase
    {
        private readonly GenderService _service;

        public GenderController(GenderService service)
            => _service = service;

        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.GetAsync(id);
            if (result == null)
                return NotFound(new { Message = "Gênero não encontrado." });

            return Ok(result);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.GetAllAsync();
            if(!result.Any())
                return NotFound(new { Message = "Nenhum gênero encontrado." });

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] GenderRegisterDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Get), new {id = created.GenderId}, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GenderRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = "Registro não encontrado" });

            return Ok(updated);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.DeleteAsync(id);
            if(!result)
                return NotFound(new { Message = "Gênero não encontrado." });

            return Ok(new { Message = "Registro deletado." });
        }
    }
}

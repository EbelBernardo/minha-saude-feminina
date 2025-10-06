using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaSaudeFeminina.DTOs.Objectives;
using MinhaSaudeFeminina.DTOs.Statuses;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Services;

namespace MinhaSaudeFeminina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly StatusService _service;

        public StatusController(StatusService service)
            => _service = service;

        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.GetAsync(id);
            if (result == null)
                return NotFound(new { Message = "Status não encontrado." });

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.GetAllAsync();
            if (!result.Any())
                return NotFound(new { Message = "Nenhum status encontrado." });

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] StatusRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Get), new {id = created.StatusId}, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StatusRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = "Status não encontrado" });

            return Ok(updated);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound(new { Message = "Status não encontrado." });

            return Ok($"Status deletado com sucesso.");
        }
    }
}

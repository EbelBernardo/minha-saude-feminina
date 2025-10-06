using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaSaudeFeminina.DTOs.Statuses;
using MinhaSaudeFeminina.DTOs.Symptoms;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Models.Relations;
using MinhaSaudeFeminina.Services;

namespace MinhaSaudeFeminina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SymptomController : ControllerBase
    {
        private readonly SymptomService _service;

        public SymptomController(SymptomService service)
            => _service = service;


        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.GetAsync(id);
            if (result == null)
                return NotFound(new { Message = "Symptom não encontrado." });

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
                return NotFound(new { Message = "Nenhum sintoma encontrado." });

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] SymptomRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Get), new {id = created.SymptomId}, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SymptomRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { Message = "Sintoma não encontrado." });

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
                return NotFound(new { Message = "Sintoma não encontrado." });

            return Ok("Simtoma deletado.");
        }
    }
}

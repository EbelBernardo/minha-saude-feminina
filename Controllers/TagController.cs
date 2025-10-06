using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Services;

namespace MinhaSaudeFeminina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly TagService _service;
        public TagController(TagService service)
            => _service = service;

        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.GetAsync(id);
            if(response == null)
                return NotFound(new {Message ="Tag não encontrada." });

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.GetAllAsync();
            if(!response.Any())
                return NotFound(new { Message = "Nenhuma tag encontrada." });

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] TagRegisterDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Get), new {id = created.TagId }, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TagRegisterDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = "Tag não encontrada" });

            return Ok(updated);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Tag não encontrada" });

            return Ok(new { message = "Tag removida com sucesso" });
        }
    }
}


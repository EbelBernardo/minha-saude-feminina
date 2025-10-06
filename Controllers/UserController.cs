using Microsoft.AspNetCore.Mvc;
using MinhaSaudeFeminina.DTOs;
using MinhaSaudeFeminina.DTOs.Users;
using MinhaSaudeFeminina.Models.UserProfile;
using MinhaSaudeFeminina.Services;

namespace MinhaSaudeFeminina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
            => _service = service;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _service.GetAsync(id);
            if(entity == null)
                return NotFound(new {Message ="Usuário não encontrado." });

            return Ok(entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entities = await _service.GetAllDtosAsync();
            if (entities == null)
                return NotFound(new { Message = "Nenhum usuário encontrado." });

            return Ok(entities);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] UserRegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Get), new { id = created.UserId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { Message = "Usuário não encontrado." });

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var deleted = await _service.DeleteAsync(id);
            if(!deleted)
                return NotFound(new { Message = "Usuário não encontrado." });

            return Ok("Usuário deletado.");
        }
    }
}

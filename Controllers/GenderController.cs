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
            => Ok(await _service.GetAsync(id));

        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllDtoAsync());

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] GenderRegisterDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new {id = created.Data!.GenderId}, created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GenderRegisterDto dto)
            => Ok(await _service.UpdateAsync(id, dto));

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _service.RemoveAsync(id));
    }
}

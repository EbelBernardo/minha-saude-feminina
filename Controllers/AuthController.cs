using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinhaSaudeFeminina.DTOs.UserAuth;
using MinhaSaudeFeminina.Models.User;
using MinhaSaudeFeminina.Services;

namespace MinhaSaudeFeminina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
            => _userService = userService;

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _userService.GetAsync(id, User));

        [Authorize (Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _userService.GetAllAsync());

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var result =  await _userService.RegisterUserAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Data!.Id}, result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
            => Ok(await _userService.LoginAsync(dto));

        [Authorize]
        [HttpPut("update-email")]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailDto dto)
            => Ok(await _userService.UpdateEmailAsync(dto, User));

        [Authorize]
        [HttpPut("update-fullname")]
        public async Task<IActionResult> UpdateFullName([FromBody] UpdateFullNameDto dto)
            => Ok(await _userService.UpdateFullNameAsync(dto, User));

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
            => Ok(await _userService.ChangePasswordAsync(dto, User));

        [HttpDelete("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _userService.DeleteAsync(id, User);
            return NoContent();
        }
    }
}

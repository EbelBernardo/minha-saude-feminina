using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.DTOs.UserAuth;
using MinhaSaudeFeminina.Exceptions;
using MinhaSaudeFeminina.Models.User;
using MinhaSaudeFeminina.Services;

namespace MinhaSaudeFeminina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

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
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            var decodedToken = Uri.UnescapeDataString(token);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
                return BadRequest("Token inválido ou expirado.");

            return Ok(new
            {
                Success = true,
                Message = "E-mail confirmado com sucesso!"
            });
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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshRequestDto dto)
            => Ok(await _userService.RefreshTokenAsync(dto.RefreshToken));

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto dto)
            => Ok(await _userService.LogoutAsync(dto.RefreshToken));
    }
}

namespace MinhaSaudeFeminina.DTOs.UserAuth
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FullName { get; set; }
    }

    public class ResponseUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; }
    }

    public class LoginUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }

    public class UpdateEmailDto
    {
        public string NewEmail { get; set; } = string.Empty;
    }

    public class UpdateFullNameDto
    {
        public string NewFullName { get; set; } = string.Empty;
    }

    public class UpdateUserDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }

    public class RefreshRequestDto
    {
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class LogoutRequestDto
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}

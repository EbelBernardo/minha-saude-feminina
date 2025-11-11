using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinhaSaudeFeminina.DTOs.Responses;
using MinhaSaudeFeminina.DTOs.UserAuth;
using MinhaSaudeFeminina.Exceptions;
using MinhaSaudeFeminina.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using ValidationException = MinhaSaudeFeminina.Exceptions.ValidationException;

namespace MinhaSaudeFeminina.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterUserDto> _registerValidator;
        private readonly IValidator<LoginUserDto> _loginValidator;
        private readonly IValidator<UpdateUserDto> _updateValidator;
        private readonly IValidator<ChangePasswordDto> _changePasswordValidator;
        private readonly IValidator<UpdateEmailDto> _updateEmailValidator;
        private readonly IValidator<UpdateFullNameDto> _updateNameValidator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserService(
            UserManager<ApplicationUser> userManager, 
            IMapper mapper, 
            IValidator<RegisterUserDto> registerValidator, 
            IValidator<LoginUserDto> loginValidator,
            IValidator<UpdateUserDto> updateValidator,
            IValidator<ChangePasswordDto> changePasswordValidator,
            IValidator<UpdateEmailDto> updateEmailValidator,
            IValidator<UpdateFullNameDto> updateNameValidator,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _updateValidator = updateValidator;
            _updateEmailValidator = updateEmailValidator;
            _updateNameValidator = updateNameValidator;
            _changePasswordValidator = changePasswordValidator;
            _emailService = emailService;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public async Task<ApiResponse<ResponseUserDto>> RegisterUserAsync(RegisterUserDto dto)
        {
            var validationResult = await _registerValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            var user = await CreateAsync(dto);

            return new ApiResponse<ResponseUserDto>
            {
                Success = true,
                Message = "Usuário criado com sucesso!",
                Data = user.Data
            };
        }

        private async Task<ApiResponse<ResponseUserDto>> CreateAsync(RegisterUserDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null) 
                throw new ValidationException(new[] { "Email já está em uso." });

            var user = _mapper.Map<ApplicationUser>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            if(!result.Succeeded)
                throw new IdentityException(result.Errors.Select(e => e.Description));

            await _userManager.AddToRoleAsync(user, "User");

            // Gera o token de confirmação de e-mail
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);
            var frontendUrl = _configuration["App:FrontendUrl"];
            var confirmUrl = $"{frontendUrl}/confirm-email?userId={user.Id}&token={encodedToken}";

            // Envia o e-mail
            var message = $@"
                <h3>Bem-vindo(a) ao Minha Saúde Feminina!</h3>
                <p>Para ativar sua conta, confirme seu e-mail clicando no link abaixo:</p>
                <a href='{confirmUrl}'>Confirmar e-mail</a>
                <p>Se você não criou esta conta, ignore esta mensagem.</p>";

            await _emailService.SendEmailAsync(user.Email!, "Confirmação de E-mail", message);

            return new ApiResponse<ResponseUserDto>
            {
                Success = true,
                Message = "Usuário criado com sucesso! Verifique seu e-mail para confirmar sua conta.",
                Data = _mapper.Map<ResponseUserDto>(user)
            };
        }

        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginUserDto dto)
        {

            var validationResult = await _loginValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if(user == null)
                throw new IdentityException(new[] { "Credenciais inválidas." });

            // Verifica se o e-mail foi confirmado
            if (!user.EmailConfirmed)
                throw new IdentityException(new[] { "E-mail ainda não confirmado. Verifique sua caixa de entrada." });

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if(!result.Succeeded)
                throw new IdentityException(new[] { "Credenciais inválidas." });

            var token = await GenerateJwtTokenAsync(user);

            return new ApiResponse<AuthResponseDto>
            {
                Success = true,
                Message = "Login realizado com sucesso",
                Data = token.Data
            };
        }

        public async Task<ApiResponse<object>> UpdateEmailAsync(UpdateEmailDto dto, ClaimsPrincipal userClaims)
        {
            var validationResult = await _updateEmailValidator.ValidateAsync(dto);
            if(!validationResult.IsValid)
                throw new Exceptions.ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            var user = await _userManager.GetUserAsync(userClaims);
            if(user == null)
                throw new IdentityException(new[] { "Usuário não encontrado." });

            user.Email = dto.NewEmail;
            user.UserName = dto.NewEmail;

            var update = await _userManager.UpdateAsync(user);
            if (!update.Succeeded)
                throw new IdentityException(update.Errors.Select(e => e.Description));

            return new ApiResponse<object>
            {
                Success = true,
                Message = "Email atualizado com sucesso!"
            };
        }

        public async Task<ApiResponse<object>> UpdateFullNameAsync(UpdateFullNameDto dto, ClaimsPrincipal userClaims)
        {
            var validationResult = await _updateNameValidator.ValidateAsync(dto);
            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            var user = await _userManager.GetUserAsync(userClaims);
            if(user == null)
                throw new IdentityException(new[] { "Usuário não encontrado." });

            user.FullName = dto.NewFullName;

            var response = await _userManager.UpdateAsync(user);
            if (!response.Succeeded)
                throw new IdentityException(response.Errors.Select(e => e.Description));

            return new ApiResponse<object>
            {
                Success = true,
                Message = "Nome atualizado com sucesso!"
            };
        }

        public async Task<ApiResponse<object>> ChangePasswordAsync(ChangePasswordDto dto, ClaimsPrincipal userClaims)
        {
            var validationResult = await _changePasswordValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            var user = await _userManager.GetUserAsync(userClaims);
            if(user == null)
                throw new IdentityException(new[] { "Usuário não encontrado." });

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
                throw new IdentityException(result.Errors.Select(e => e.Description));

            return new ApiResponse<object>
            {
                Success = true,
                Message = "Senha alterada com sucesso!"
            };
        }

        public async Task<ApiResponse<AuthResponseDto>> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new ApiResponse<AuthResponseDto>
            {
                Success = true,
                Message = "Token gerado com sucesso!",
                Data = new AuthResponseDto
                { 
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo.ToLocalTime()
                }
            };
        }

        public async Task<ApiResponse<ResponseUserDto>> GetAsync(int id, ClaimsPrincipal userClaims)
        {
            if(id<=0) 
                throw new ValidationException(new[] { "Id inválido." });

            var currentUser = await _userManager.GetUserAsync(userClaims);
            if (currentUser == null)
                throw new IdentityException(new[] { "Usuário não encontrado." });

            var roles = await _userManager.GetRolesAsync(currentUser);
            bool isAdmin = roles.Contains("Admin");

            if (!isAdmin && currentUser.Id != id)
                throw new IdentityException(new[] { "Acesso negado." });

            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null) 
                throw new IdentityException(new[] { "Usuário não encontrado." });

            return new ApiResponse<ResponseUserDto>()
            {
                Success = true,
                Data = _mapper.Map<ResponseUserDto>(user)
            };
        }

        public async Task<ApiResponse<IEnumerable<ResponseUserDto>>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            return new ApiResponse<IEnumerable<ResponseUserDto>>
            {
                Success = true,
                Data = _mapper.Map<IEnumerable<ResponseUserDto>>(users)
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id, ClaimsPrincipal userClaims)
        {
            if(id <= 0) 
                throw new ValidationException(new[] { "Id inválido." });

            var currentUser = await _userManager.GetUserAsync(userClaims);
            if (currentUser == null)
                throw new IdentityException(new[] { "Usuário não encontrado." });

            var roles = await _userManager.GetRolesAsync(currentUser);
            bool isAdmin = roles.Contains("Admin");

            if (!isAdmin)
                throw new IdentityException(new[] { "Acesso negado." });

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) 
                throw new IdentityException(new[] { "Usuário não encontrado." });

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new IdentityException(result.Errors.Select(e => e.Description));

            return new ApiResponse<bool>
            { 
                Success = true,
                Message = "Usuário removido com sucesso!",
                Data = true
            };

        }
    }
}

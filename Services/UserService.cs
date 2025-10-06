using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs.Users;
using MinhaSaudeFeminina.Models.UserProfile;
using MinhaSaudeFeminina.Services.Base;

namespace MinhaSaudeFeminina.Services
{
    public class UserService : EntityService<User>
    {
        private readonly IMapper _mapper;
        public UserService(AppDbContext context, IMapper mapper) : base(context)
            => _mapper = mapper;

        public async Task<UserResponseDto?> GetAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return null;

            return _mapper.Map<UserResponseDto>(entity);
        }

        public async Task<List<UserResponseDto>?> GetAllDtosAsync()
        {
            var entities = await base.GetAllAsync();
            if(!entities.Any()) return null;

            return _mapper.Map<List<UserResponseDto>>(entities);
        }

        public async Task<UserResponseDto> CreateAsync(UserRegisterDto dto)
        {
            //user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordhash);
            var user = _mapper.Map<User>(dto);
            var created = await base.CreateAsync(user);
            return _mapper.Map<UserResponseDto>(created);
        }

        public async Task<UserResponseDto?> UpdateAsync(int id, UserRegisterDto dto)
        {
            var user = await base.GetByIdAsync(id);
            if (user == null) return null;

            _mapper.Map(dto, user);
            await base.UpdateAsync(user);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<bool> RemoveAsync(int id)
            =>await base.DeleteAsync(id);
    }
}

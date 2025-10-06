using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs.Profiles;
using MinhaSaudeFeminina.DTOs.Users;
using MinhaSaudeFeminina.Models.UserProfile;
using MinhaSaudeFeminina.Services.Base;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MinhaSaudeFeminina.Services
{
    public class ProfileService : EntityService<Models.UserProfile.Profile>
    {
        private readonly IMapper _mapper;

        public ProfileService(IMapper mapper, AppDbContext context) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ProfileResponseDto?> GetAsync(int id)
        {
            var entity = await base.GetByIdAsync(id);
            if (entity == null) return null;

            return _mapper.Map<ProfileResponseDto>(entity);
        }

        public async Task<List<ProfileResponseDto>?> GetAllDtosAsync()
        {
            var entities = await base.GetAllAsync();

            return _mapper.Map<List<ProfileResponseDto>>(entities);
        }

        public async Task<ProfileResponseDto> CreateAsync(ProfileRegisterDto dto, int userId)
        {
            var profile = _mapper.Map<Models.UserProfile.Profile>(dto);
            profile.UserId = userId;

            var created = await base.CreateAsync(profile);
            return _mapper.Map<ProfileResponseDto>(created);
        }

        public async Task<ProfileResponseDto?> UpdateAsync(int id, ProfileRegisterDto dto)
        {
            var exists = await base.GetByIdAsync(id);
            if (exists == null) return null;

            _mapper.Map(dto, exists);
            await base.UpdateAsync(exists);
            return _mapper.Map<ProfileResponseDto>(exists);
        }

        public async Task<bool> RemoveAsync(int id)
            => await base.DeleteAsync(id);
    }
}

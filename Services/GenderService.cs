using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Controllers;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs;
using MinhaSaudeFeminina.DTOs.Gender;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Services.Base;

namespace MinhaSaudeFeminina.Services
{
    public class GenderService : EntityService<Gender>
    {
        private readonly IMapper _mapper;
        public GenderService(AppDbContext context, IMapper mapper) : base(context)
            => _mapper = mapper;

        public async Task<GenderResponseDto?> GetAsync(int id)
        {
            var entity = await base.GetByIdAsync(id);
            if (entity == null) return null;

            return _mapper.Map<GenderResponseDto>(entity);
        }

        public async Task<List<GenderResponseDto>> GetAllDtosAsync()
        {
            var entities = await base.GetAllAsync();

            return _mapper.Map<List<GenderResponseDto>>(entities);
        }

        public async Task<GenderResponseDto> CreateAsync(GenderRegisterDto dto)
        {
            var gender = _mapper.Map<Gender>(dto);
            var created = await base.CreateAsync(gender);
            return _mapper.Map<GenderResponseDto>(created);
        }

        public async Task<GenderResponseDto?> UpdateAsync(int id, GenderRegisterDto dto)
        {
            var gender = await base.GetByIdAsync(id);
            if(gender == null) return null;

            _mapper.Map(dto, gender);
            await base.UpdateAsync(gender);
            return _mapper.Map<GenderResponseDto>(gender);
        }

        public async Task<bool> RemoveAsync(int id)
            => await base.DeleteAsync(id);
    }
}

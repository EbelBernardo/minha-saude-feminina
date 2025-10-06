using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs.Objectives;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Services.Base;

namespace MinhaSaudeFeminina.Services
{
    public class ObjectiveService : EntityService<Objective>
    {
        private readonly IMapper _mapper;

        public ObjectiveService(IMapper mapper, AppDbContext context) : base(context)
            => _mapper = mapper;

        public async Task<ObjectiveResponseDto?> GetAsync(int id)
        {
            var entity = await base.GetByIdAsync(id);
            if (entity == null) return null;

            return _mapper.Map<ObjectiveResponseDto>(entity);
        }

        public async Task<List<ObjectiveResponseDto>> GetAllDtosAsync()
        {
            var entities = await base.GetAllAsync();

            return _mapper.Map<List<ObjectiveResponseDto>>(entities);
        }

        public async Task<ObjectiveResponseDto> CreateAsync(ObjectiveRegisterDto dto)
        {
            var entity = _mapper.Map<Objective>(dto);
            var created = await base.CreateAsync(entity);
            return _mapper.Map<ObjectiveResponseDto>(created);
        }

        public async Task<ObjectiveResponseDto?> UpdateAsync(int id, ObjectiveRegisterDto dto)
        {
            var exists = await base.GetByIdAsync(id);
            if (exists == null) return null;

            _mapper.Map(dto, exists);
            await base.UpdateAsync(exists);
            return _mapper.Map<ObjectiveResponseDto>(exists);
        }

        public async Task<bool> RemoveAsync(int id)
            => await base.DeleteAsync(id);
    }
}

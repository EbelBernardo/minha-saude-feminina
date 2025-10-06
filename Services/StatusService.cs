using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs.Statuses;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Services.Base;

namespace MinhaSaudeFeminina.Services
{
    public class StatusService : EntityService<Status>
    {
        private readonly IMapper _mapper;
        public StatusService(IMapper mapper, AppDbContext context) : base(context)
            => _mapper = mapper;

        public async Task<StatusResponseDto?> GetAsync(int id)
        {
            var entity = await base.GetByIdAsync(id);
            if (entity == null) return null;

            return _mapper.Map<StatusResponseDto>(entity);
        }

        public async Task<List<StatusResponseDto>> GetAllDtosAsync()
        {
            var entities = await base.GetAllAsync();

            return _mapper.Map<List<StatusResponseDto>>(entities);
        }

        public async Task<StatusResponseDto> CreateAsync(StatusRegisterDto dto)
        {
            var symptom = _mapper.Map<Status>(dto);
            var created = await base.CreateAsync(symptom);
            return _mapper.Map<StatusResponseDto>(created);
        }

        public async Task<StatusResponseDto?> UpdateAsync(int id, StatusRegisterDto dto)
        {
            var exists = await base.GetByIdAsync(id);
            if (exists == null) return null;

            _mapper.Map(dto, exists);
            await base.UpdateAsync(exists);
            return _mapper.Map<StatusResponseDto>(exists);
        }

        public async Task<bool> RemoveAsync(int id)
            => await base.DeleteAsync(id);
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs.Statuses;
using MinhaSaudeFeminina.DTOs.Symptoms;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Models.Relations;
using MinhaSaudeFeminina.Services.Base;
using System.Reflection.Metadata.Ecma335;

namespace MinhaSaudeFeminina.Services
{
    public class SymptomService : EntityService<Symptom>
    {
        private readonly IMapper _mapper;
        public SymptomService(IMapper mapper, AppDbContext context) : base(context)
            => _mapper = mapper;

        public async Task<SymptomResponseDto?> GetAsync(int id)
        {
            var entity = await base.GetByIdAsync(id);
            if (entity == null) return null;

            return _mapper.Map<SymptomResponseDto>(entity);
        }

        public async Task<List<SymptomResponseDto>> GetAllDtosAsync()
        {
            var entities = await base.GetAllAsync();

            return _mapper.Map<List<SymptomResponseDto>>(entities);
        }

        public async Task<SymptomResponseDto> CreateAsync(SymptomRegisterDto dto)
        {
            var symptom = _mapper.Map<Symptom>(dto);
            if (dto.TagId != null && dto.TagId.Any())
            {
                var tags = await _context.Tags
                    .Where(t => dto.TagId.Contains(t.TagId))
                    .ToListAsync();

                symptom.TagSymptoms = tags
                    .Select(tag => new TagSymptom
                    {
                        Tag = tag,
                        Symptom = symptom
                    }).ToList();
            }

            var created = await base.CreateAsync(symptom);
            return _mapper.Map<SymptomResponseDto>(created);
        }

        public async Task<SymptomResponseDto?> UpdateAsync(int id, SymptomRegisterDto dto)
        {
            var exists = await _context.Symptoms
        .Include(s => s.TagSymptoms)
        .ThenInclude(ts => ts.Tag)
        .FirstOrDefaultAsync(s => s.SymptomId == id);

            if (exists == null) return null;

            // Atualiza os campos básicos
            _mapper.Map(dto, exists);

            // Atualiza as Tags (N:N)
            exists.TagSymptoms.Clear(); // remove as antigas relações

            if (dto.TagId != null && dto.TagId.Any())
            {
                var tags = await _context.Tags
                    .Where(t => dto.TagId.Contains(t.TagId))
                    .ToListAsync();

                exists.TagSymptoms = tags
                    .Select(tag => new TagSymptom
                    {
                        SymptomId = exists.SymptomId,
                        TagId = tag.TagId
                    }).ToList();
            }

            await base.UpdateAsync(exists);
            return _mapper.Map<SymptomResponseDto>(exists);
        }

        public async Task<bool> RemoveAsync(int id)
            => await base.DeleteAsync(id);
    }
}

using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs.Responses;
using MinhaSaudeFeminina.DTOs.Statuses;
using MinhaSaudeFeminina.DTOs.Symptoms;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.Exceptions;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Models.Relations;
using MinhaSaudeFeminina.Services.Base;
using System.Reflection.Metadata.Ecma335;
using ValidationException = MinhaSaudeFeminina.Exceptions.ValidationException;


namespace MinhaSaudeFeminina.Services
{
    public class SymptomService : EntityService<Symptom>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<SymptomRegisterDto> _validator;
        public SymptomService(IMapper mapper, AppDbContext context, IValidator<SymptomRegisterDto> validator) : base(context)
        {
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse<SymptomResponseDto>> GetAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var entity = await base.GetByIdAsync(id);
            if (entity == null)
                throw new BusinessException("Sintoma não encontrado.");

            return new ApiResponse<SymptomResponseDto>
            {
                Success = true,
                Data = _mapper.Map<SymptomResponseDto>(entity)
            };
        }

        public async Task<ApiResponse<List<SymptomResponseDto>>> GetAllDtosAsync()
        {
            var entities = await base.GetAllAsync();

            return new ApiResponse<List<SymptomResponseDto>>
            {
                Success = true,
                Data = _mapper.Map<List<SymptomResponseDto>>(entities)
            };
        }

        public async Task<ApiResponse<SymptomResponseDto>> CreateAsync(SymptomRegisterDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            if(await base.AnyAsync(s => s.Title == dto.Title))
                throw new BusinessException(
                    new[] { "Já existe um Sintoma com este nome." },
                    "Não foi possível criar o Sintoma.");

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
            return new ApiResponse<SymptomResponseDto>
            {
                Success = true,
                Message = "Sintoma criado com sucesso.",
                Data = _mapper.Map<SymptomResponseDto>(created)
            };
        }

        public async Task<ApiResponse<SymptomResponseDto>> UpdateAsync(int id, SymptomRegisterDto dto)
        {
            if(id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var exists = await _context.Symptoms
                .Include(s => s.TagSymptoms)
                .ThenInclude(ts => ts.Tag)
                .FirstOrDefaultAsync(s => s.SymptomId == id);

            if (exists == null)
                throw new BusinessException("Sintoma não encontrado.");

            if(await base.AnyAsync(s => s.Title == dto.Title && s.SymptomId != id))
                throw new BusinessException(
                    new[] { "Já existe um Sintoma com este nome." },
                    "Não foi possível atualizar o Sintoma.");

            _mapper.Map(dto, exists);

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
            return new ApiResponse<SymptomResponseDto>
            {
                Success = true,
                Message = "Sintoma atualizado com sucesso.",
                Data = _mapper.Map<SymptomResponseDto>(exists)
            };
        }

        public async Task<ApiResponse<bool>> RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var status = await base.GetByIdAsync(id);
            if (status == null)
                throw new BusinessException("Sintoma não encontrado.");

            await base.DeleteAsync(id);
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Sintoma removido com sucesso.",
                Data = true
            };
        }
    }
}

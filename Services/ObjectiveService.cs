using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs.Objectives;
using MinhaSaudeFeminina.DTOs.Responses;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.Exceptions;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Services.Base;
using ValidationException = MinhaSaudeFeminina.Exceptions.ValidationException;

namespace MinhaSaudeFeminina.Services
{
    public class ObjectiveService : EntityService<Objective>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<ObjectiveRegisterDto> _validator;

        public ObjectiveService(IMapper mapper, AppDbContext context, IValidator<ObjectiveRegisterDto> validator) : base(context)
        {
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse<ObjectiveResponseDto>> GetAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var entity = await base.GetByIdAsync(id);
            if (entity == null)
                throw new BusinessException("Objetivo não encontrado.");

            return new ApiResponse<ObjectiveResponseDto>
            {
                Success = true,
                Data = _mapper.Map<ObjectiveResponseDto>(entity)
            };
        }

        public async Task<ApiResponse<List<ObjectiveResponseDto>>> GetAllDtosAsync()
        {
            var entities = await base.GetAllAsync();

            return new ApiResponse<List<ObjectiveResponseDto>>
            {
                Success = true,
                Data = _mapper.Map<List<ObjectiveResponseDto>>(entities)
            };
        }

        public async Task<ApiResponse<ObjectiveResponseDto>> CreateAsync(ObjectiveRegisterDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            if (await base.AnyAsync(c => c.Title == dto.Title))
                throw new BusinessException(
                    new[] { "Já existe um Objectivo com este nome." },
                    "Não foi possível criar o Objectivo");

            var entity = _mapper.Map<Objective>(dto);
            var created = await base.CreateAsync(entity);

            return new ApiResponse<ObjectiveResponseDto>()
            {
                Success = true,
                Message = "Objetivo criado com sucesso.",
                Data = _mapper.Map<ObjectiveResponseDto>(created)
            };
        }

        public async Task<ApiResponse<ObjectiveResponseDto>> UpdateAsync(int id, ObjectiveRegisterDto dto)
        {
            if (id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var exists = await base.GetByIdAsync(id);
            if (exists == null)
                throw new BusinessException("Objetivo não encontrado.");

            if (await base.AnyAsync(c => c.Title == dto.Title && c.ObjectiveId != id))
                throw new BusinessException(
                    new[] { "Já existe um Objetivo com este nome." },
                    "Não foi possível atualizar o Objetivo.");

            _mapper.Map(dto, exists);
            await base.UpdateAsync(exists);
            return new ApiResponse<ObjectiveResponseDto>
            {
                Success = true,
                Message = "Objetivo atualizado com sucesso.",
                Data = _mapper.Map<ObjectiveResponseDto>(exists)
            };
        }

        public async Task<ApiResponse<bool>> RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var exists = await base.GetByIdAsync(id);
            if (exists == null)
                throw new BusinessException("Objetivo não encontrado.");

            await base.DeleteAsync(id);
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Objetivo removido com sucesso.",
                Data = true
            };
        }
    }
}

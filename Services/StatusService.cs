using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs.Gender;
using MinhaSaudeFeminina.DTOs.Responses;
using MinhaSaudeFeminina.DTOs.Statuses;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.Exceptions;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Services.Base;
using System.Reflection;
using ValidationException = MinhaSaudeFeminina.Exceptions.ValidationException;

namespace MinhaSaudeFeminina.Services
{
    public class StatusService : EntityService<Status>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<StatusRegisterDto> _validator;
        public StatusService(IMapper mapper, AppDbContext context, IValidator<StatusRegisterDto> validator) : base(context)
        {
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse<StatusResponseDto>> GetAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var entity = await base.GetByIdAsync(id);
            if (entity == null)
                throw new BusinessException("Status não encontrado.");

            return new ApiResponse<StatusResponseDto>
            {
                Success = true,
                Data = _mapper.Map<StatusResponseDto>(entity)
            };
        }

        public async Task<ApiResponse<List<StatusResponseDto>>> GetAllDtosAsync()
        {
            var entities = await base.GetAllAsync();

            return new ApiResponse<List<StatusResponseDto>>
            {
                Success = true,
                Data = _mapper.Map<List<StatusResponseDto>>(entities)
            };
        }

        public async Task<ApiResponse<StatusResponseDto>> CreateAsync(StatusRegisterDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            if(await base.AnyAsync(s => s.Title == dto.Title))
                throw new BusinessException(
                    new[] { "Já existe um Status cadastrado com esse nome." },
                    "Não foi possível criar o Status");

            var status = _mapper.Map<Status>(dto);
            var created = await base.CreateAsync(status);

            return new ApiResponse<StatusResponseDto>
            {
                Success = true,
                Message = "Status criado com sucesso.",
                Data = _mapper.Map<StatusResponseDto>(created)
            };
        }

        public async Task<ApiResponse<StatusResponseDto>> UpdateAsync(int id, StatusRegisterDto dto)
        {
            if(id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            var exists = await base.GetByIdAsync(id);
            if (exists == null) 
                throw new BusinessException("Status não encontrado.");

            if (await base.AnyAsync(g => g.Title == dto.Title && g.StatusId != id))
                throw new BusinessException(
                    new[] { "Já existe um Status com este nome." },
                    "Não foi possível atualizar o Status.");


            _mapper.Map(dto, exists);
            await base.UpdateAsync(exists);
            return new ApiResponse<StatusResponseDto>
            {
                Success = true,
                Message = "Status atualizado com sucesso.",
                Data = _mapper.Map<StatusResponseDto>(exists)
            };
        }

        public async Task<ApiResponse<bool>> RemoveAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var status = await base.GetByIdAsync(id);
            if (status == null)
                throw new BusinessException("Status não encontrado.");

            await base.DeleteAsync(id);
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Status removido com sucesso.",
                Data = true
            };
        }
    }
}

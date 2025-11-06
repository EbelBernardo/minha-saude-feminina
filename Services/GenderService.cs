using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Controllers;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs;
using MinhaSaudeFeminina.DTOs.Gender;
using MinhaSaudeFeminina.DTOs.Responses;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.Exceptions;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Services.Base;
using ValidationException = MinhaSaudeFeminina.Exceptions.ValidationException;

namespace MinhaSaudeFeminina.Services
{
    public class GenderService : EntityService<Gender>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<GenderRegisterDto> _validator;
        public GenderService(AppDbContext context, IMapper mapper, IValidator<GenderRegisterDto> validator) : base(context)
        {
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiResponse<GenderResponseDto>> GetAsync(int id)
        {
            if(id <= 0 )
                throw new ValidationException(new[] {"Id inválido."});

            var entity = await base.GetByIdAsync(id);
            if (entity == null)
                throw new BusinessException("Gênero não encontrado.");

            return new ApiResponse<GenderResponseDto>
            {
                Success = true,
                Data = _mapper.Map<GenderResponseDto>(entity)
            };
        }

        public async Task<ApiResponse<List<GenderResponseDto>>> GetAllDtoAsync()
        {
            var entities = await base.GetAllAsync();

            return new ApiResponse<List<GenderResponseDto>>
            {
                Success = true,
                Data = _mapper.Map<List<GenderResponseDto>>(entities)
            };
        }

        public async Task<ApiResponse<GenderResponseDto>> CreateAsync(GenderRegisterDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            if (await base.AnyAsync(e => e.Title == dto.Title))
                throw new BusinessException(
                    new[] { "Já existe um Gênero com este nome." },
                    "Não foi possível criar o Gênero.");

            var gender = _mapper.Map<Gender>(dto);
            var created = await base.CreateAsync(gender);

            return new ApiResponse<GenderResponseDto>
            {
                Success = true,
                Message = "Gênero criado com sucesso.",
                Data = _mapper.Map<GenderResponseDto>(created)
            };
        }

        public async Task<ApiResponse<GenderResponseDto>> UpdateAsync(int id, GenderRegisterDto dto)
        {
            if(id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            var gender = await base.GetByIdAsync(id);
            if(gender == null)
                throw new BusinessException(
                    "Gênero não encontrado");

            if(await base.AnyAsync(g => g.Title == dto.Title && g.GenderId != id))
                throw new BusinessException(
                    new[] {"Já existe um Gênero com este nome." },
                    "Não foi possível atualizar o Gênero.");

            _mapper.Map(dto, gender);
            await base.UpdateAsync(gender);
            return new ApiResponse<GenderResponseDto>
            {
                Success = true,
                Message = "Gênero atualizado com sucesso.",
                Data = _mapper.Map<GenderResponseDto>(gender)
            };
        }

        public async Task<ApiResponse<bool>> RemoveAsync(int id)
        {
            if(id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var gender = await base.GetByIdAsync(id);
            if(gender == null)
                throw new BusinessException("Gênero não encontrado.");

            await base.DeleteAsync(id);
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Gênero removido com sucesso.",
                Data = true
            };
        }
    }
}

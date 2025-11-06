using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs.Profiles;
using MinhaSaudeFeminina.DTOs.Responses;
using MinhaSaudeFeminina.Exceptions;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Models.Relations;
using MinhaSaudeFeminina.Models.UserProfile;
using MinhaSaudeFeminina.Services.Base;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ValidationException = MinhaSaudeFeminina.Exceptions.ValidationException;


namespace MinhaSaudeFeminina.Services
{
    public class ProfileService : EntityService<Models.UserProfile.Profile>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<ProfileRegisterDto> _validator;

        public ProfileService(IMapper mapper, AppDbContext context, IValidator<ProfileRegisterDto> validator) : base(context)
        {
            _mapper = mapper;
            _validator = validator;
        }

        private System.Linq.IQueryable<Models.UserProfile.Profile> GetProfileQuery()
        {
            return _context.Profiles
                .Include(p => p.ProfileGenders).ThenInclude(pg => pg.Gender)
                .Include(p => p.ProfileObjectives).ThenInclude(po => po.Objective)
                .Include(p => p.ProfileStatuses).ThenInclude(ps => ps.Status)
                .Include(p => p.ProfileSymptoms).ThenInclude(ps => ps.Symptom);
        }

        public async Task<ApiResponse<ProfileResponseDto>> GetAsync(int id)
        {
            if(id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var profile = await GetProfileQuery()
                .FirstOrDefaultAsync(p => p.ProfileId == id);

            //var entity = await base.GetByIdAsync(id);
            if (profile == null)
                throw new BusinessException("Perfil não encontrado.");

            return new ApiResponse<ProfileResponseDto>
            {
                Success = true,
                Data = _mapper.Map<ProfileResponseDto>(profile)
            };
        }

        public async Task<ApiResponse<List<ProfileResponseDto>>> GetAllDtosAsync()
        {
            var entities = await GetProfileQuery().ToListAsync();

            return new ApiResponse<List<ProfileResponseDto>>
            {
                Success = true,
                Data = _mapper.Map<List<ProfileResponseDto>>(entities)
            };
        }

        public async Task<ApiResponse<ProfileResponseDto>> CreateAsync(ProfileRegisterDto dto, int userId)
        {
            var validationResult = await _validator.ValidateAsync(dto, options => options.IncludeRuleSets("Create"));
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            var profile = _mapper.Map<Models.UserProfile.Profile>(dto);
            profile.UserId = userId;

            var gender = await _context.Genders
                .Where(g => dto.Genders.Contains(g.Title))
                .ToListAsync();
            profile.ProfileGenders = gender.Select(g => new ProfileGender {Profile = profile, GenderId = g.GenderId }).ToList();

            var objective = await _context.Objectives
                .Where(o => dto.Objectives.Contains(o.Title))
                .ToListAsync();
            profile.ProfileObjectives = objective.Select(o => new ProfileObjective { Profile = profile, ObjectiveId = o.ObjectiveId }).ToList();

            var status = await _context.Statuses
                .Where(s => dto.Statuses.Contains(s.Title))
                .ToListAsync();
            profile.ProfileStatuses = status.Select(s => new ProfileStatus { Profile = profile, StatusId = s.StatusId }).ToList();

            var symptom = await _context.Symptoms
                .Where(sy => dto.Symptoms.Contains(sy.Title))
                .ToListAsync();
            profile.ProfileSymptoms = symptom.Select(sy => new ProfileSymptom { Profile = profile, SymptomId = sy.SymptomId }).ToList();

            var created = await base.CreateAsync(profile);

            return new ApiResponse<ProfileResponseDto>
            {
                Success = true,
                Data = _mapper.Map<ProfileResponseDto>(created)
            };
        }

        public async Task<ApiResponse<ProfileResponseDto>> UpdateAsync(int id, ProfileRegisterDto dto)
        {
            if(id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var validationResult = await _validator.ValidateAsync(dto, options => options.IncludeRuleSets("Update"));
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.Select(e => e.ErrorMessage));

            var exists = await _context.Profiles
                .Include(p=>p.ProfileGenders)
                .Include(p=>p.ProfileObjectives)
                .Include(p=>p.ProfileStatuses)
                .Include(p=>p.ProfileSymptoms)
                .FirstOrDefaultAsync(p=> p.ProfileId == id);
            if (exists == null) 
                throw new BusinessException("Perfil não encontrado.");

            _mapper.Map(dto, exists);

            var gender = await _context.Genders
                .Where(g => dto.Genders.Contains(g.Title))
                .ToListAsync();
            exists.ProfileGenders = gender.Select(g => new ProfileGender { GenderId = g.GenderId }).ToList();

            var objective = await _context.Objectives
                .Where(o => dto.Objectives.Contains(o.Title))
                .ToListAsync();
            exists.ProfileObjectives = objective.Select(o => new ProfileObjective { ObjectiveId = o.ObjectiveId }).ToList();

            var status = await _context.Statuses
                .Where(s => dto.Statuses.Contains(s.Title))
                .ToListAsync();
            exists.ProfileStatuses = status.Select(s => new ProfileStatus { StatusId = s.StatusId }).ToList();

            var symptom = await _context.Symptoms
                .Where(sy => dto.Symptoms.Contains(sy.Title))
                .ToListAsync();
            exists.ProfileSymptoms = symptom.Select(sy => new ProfileSymptom { SymptomId = sy.SymptomId }).ToList();

            await base.UpdateAsync(exists);

            return new ApiResponse<ProfileResponseDto>
            {
                Success = true,
                Data = _mapper.Map<ProfileResponseDto>(exists)
            };
        }

        public async Task<ApiResponse<bool>> RemoveAsync(int id)
        {
            if(id <= 0)
                throw new ValidationException(new[] { "Id inválido." });

            var profile = await _context.Profiles
                .Include(p => p.ProfileGenders)
                .Include(p => p.ProfileObjectives)
                .Include(p => p.ProfileStatuses)
                .Include(p => p.ProfileSymptoms)
                .FirstOrDefaultAsync(p => p.ProfileId == id);
            if(profile == null)
                throw new BusinessException("Perfil não encontrado.");

            await _context.SaveChangesAsync();

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Perfil removido com sucesso.",
                Data = true
            };
        }
    }
}

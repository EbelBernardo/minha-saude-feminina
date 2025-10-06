using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinhaSaudeFeminina.Controllers;
using MinhaSaudeFeminina.Data;
using MinhaSaudeFeminina.DTOs.Statuses;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Services.Base;

namespace MinhaSaudeFeminina.Services
{
    public class TagService : EntityService<Tag>
    {
        private readonly IMapper _mapper;

        public TagService(AppDbContext context, IMapper mapper) : base(context)
            => _mapper = mapper;

        public async Task<TagResponseDto?> GetAsync(int id)
        {
            var entity = await base.GetByIdAsync(id);
            if (entity == null) return null;

            return _mapper.Map<TagResponseDto>(entity);
        }

        public async Task<List<TagResponseDto>?> GetAllDtosAsync()
        {
            var entities = await base.GetAllAsync();

            return _mapper.Map<List<TagResponseDto>>(entities);
        }

        public async Task<TagResponseDto> CreateAsync(TagRegisterDto dto)
        {
            var tag = _mapper.Map<Tag>(dto);
            var created = await base.CreateAsync(tag);
            return _mapper.Map<TagResponseDto>(created);
        }

        public async Task<TagResponseDto?> UpdateAsync(int id, TagRegisterDto dto)
        {
            var tag = await base.GetByIdAsync(id);
            if (tag == null) return null;

            _mapper.Map(dto, tag);
            await base.UpdateAsync(tag);
            return _mapper.Map<TagResponseDto>(tag);
        }

        public async Task<bool> RemoveAsync(int id)
            => await base.DeleteAsync(id);
    }
}

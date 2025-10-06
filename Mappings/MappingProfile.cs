using AutoMapper;
using MinhaSaudeFeminina.DTOs.Gender;
using MinhaSaudeFeminina.DTOs.Objectives;
using MinhaSaudeFeminina.DTOs.Profiles;
using MinhaSaudeFeminina.DTOs.Statuses;
using MinhaSaudeFeminina.DTOs.Symptoms;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.DTOs.Users;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Models.Relations;
using MinhaSaudeFeminina.Models.UserProfile;
using Profile = MinhaSaudeFeminina.Models.UserProfile.Profile;

namespace MinhaSaudeFeminina.Mappings
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            //User
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserResponseDto>();

            //Profile
            CreateMap<ProfileRegisterDto, Profile>();
            CreateMap<Profile, ProfileResponseDto>();

            //Status
            CreateMap<StatusRegisterDto, Status>();
            CreateMap<Status, StatusResponseDto>();

            //Objective
            CreateMap<ObjectiveRegisterDto, Objective>();
            CreateMap<Objective, ObjectiveResponseDto>();

            //Gender
            CreateMap<GenderRegisterDto, Gender>();
            CreateMap<Gender, GenderResponseDto>();

            //Tag
            CreateMap<TagRegisterDto, Tag>();
            CreateMap<Tag, TagResponseDto>();

            //Symptom
            CreateMap<SymptomRegisterDto, Symptom>()
                .ForMember(dest => dest.TagSymptoms, opt => opt.MapFrom(src => src.TagId.Select(id => new TagSymptom { TagId = id })));
            CreateMap<Symptom, SymptomResponseDto>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.TagSymptoms.Select(ts => ts.Tag)));
        }
    }
}

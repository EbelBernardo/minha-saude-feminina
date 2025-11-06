using AutoMapper;
using MinhaSaudeFeminina.DTOs.Gender;
using MinhaSaudeFeminina.DTOs.Objectives;
using MinhaSaudeFeminina.DTOs.Profiles;
using MinhaSaudeFeminina.DTOs.Statuses;
using MinhaSaudeFeminina.DTOs.Symptoms;
using MinhaSaudeFeminina.DTOs.Tags;
using MinhaSaudeFeminina.DTOs.UserAuth;
using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Models.Relations;
using MinhaSaudeFeminina.Models.User;
using MinhaSaudeFeminina.Models.UserProfile;
using Profile = MinhaSaudeFeminina.Models.UserProfile.Profile;

namespace MinhaSaudeFeminina.Mappings
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            //User
            CreateMap<RegisterUserDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<ApplicationUser, ResponseUserDto>();

            CreateMap<UpdateUserDto, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            //CreateMap<ChangePasswordDto, ApplicationUser>()
            //    .ForAllMembers(opt => opt.Ignore());

            //Profile
            CreateMap<ProfileRegisterDto, Profile>();
            CreateMap<Profile, ProfileResponseDto>()
                .ForMember(dest => dest.Genders, opt => opt.MapFrom(src =>
                    src.ProfileGenders != null
                        ? src.ProfileGenders
                            .Where(pg => pg.Gender != null)
                            .Select(pg => pg.Gender.Title)
                            .ToList()
                        : new List<string>()
                ))
                .ForMember(dest => dest.Statuses, opt => opt.MapFrom(src =>
                    src.ProfileStatuses != null
                        ? src.ProfileStatuses
                            .Where(ps => ps.Status != null)
                            .Select(ps => ps.Status.Title)
                            .ToList()
                        : new List<string>()
                ))
                .ForMember(dest => dest.Objectives, opt => opt.MapFrom(src =>
                    src.ProfileObjectives != null
                        ? src.ProfileObjectives
                            .Where(po => po.Objective != null)
                            .Select(po => po.Objective.Title)
                            .ToList()
                        : new List<string>()
                ))
                .ForMember(dest => dest.Symptoms, opt => opt.MapFrom(src =>
                    src.ProfileSymptoms != null
                        ? src.ProfileSymptoms
                            .Where(ps => ps.Symptom != null)
                            .Select(ps => ps.Symptom.Title)
                            .ToList()
                        : new List<string>()
                ));


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

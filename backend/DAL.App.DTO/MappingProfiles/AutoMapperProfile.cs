using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.App.DTO.Company, Domain.App.Company>().ReverseMap();
            CreateMap<DAL.App.DTO.Compatibility, Domain.App.Compatibility>().ReverseMap();
            CreateMap<DAL.App.DTO.CompatibilityType, Domain.App.CompatibilityType>().ReverseMap();
            CreateMap<DAL.App.DTO.Configuration, Domain.App.Configuration>().ReverseMap();
            CreateMap<DAL.App.DTO.Emulator, Domain.App.Emulator>().ReverseMap();
            CreateMap<DAL.App.DTO.Game, Domain.App.Game>().ReverseMap();
            CreateMap<DAL.App.DTO.GameGenre, Domain.App.GameGenre>().ReverseMap();
            CreateMap<DAL.App.DTO.GameOnPlatform, Domain.App.GameOnPlatform>().ReverseMap();
            CreateMap<DAL.App.DTO.Genre, Domain.App.Genre>().ReverseMap();
            CreateMap<DAL.App.DTO.Media, Domain.App.Media>().ReverseMap();
            CreateMap<DAL.App.DTO.MediaType, Domain.App.MediaType>().ReverseMap();
            CreateMap<DAL.App.DTO.Platform, Domain.App.Platform>().ReverseMap();
            CreateMap<DAL.App.DTO.PlatformType, Domain.App.PlatformType>().ReverseMap();
            CreateMap<DAL.App.DTO.Progress, Domain.App.Progress>().ReverseMap();
           
            CreateMap<DAL.App.DTO.Identity.AppRole, Domain.App.Identity.AppRole>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser>().ReverseMap();
        }
    }
}
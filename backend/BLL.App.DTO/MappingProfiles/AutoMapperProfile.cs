using AutoMapper;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Company, DAL.App.DTO.Company>().ReverseMap();
            CreateMap<Compatibility, DAL.App.DTO.Compatibility>().ReverseMap();
            CreateMap<CompatibilityType, DAL.App.DTO.CompatibilityType>().ReverseMap();
            CreateMap<Configuration, DAL.App.DTO.Configuration>().ReverseMap();
            CreateMap<Emulator, DAL.App.DTO.Emulator>().ReverseMap();
            CreateMap<Game, DAL.App.DTO.Game>().ReverseMap();
            CreateMap<GameGenre, DAL.App.DTO.GameGenre>().ReverseMap();
            CreateMap<GameOnPlatform, DAL.App.DTO.GameOnPlatform>().ReverseMap();
            CreateMap<Genre, DAL.App.DTO.Genre>().ReverseMap();
            CreateMap<Media, DAL.App.DTO.Media>().ReverseMap();
            CreateMap<MediaType, DAL.App.DTO.MediaType>().ReverseMap();
            CreateMap<Platform, DAL.App.DTO.Platform>().ReverseMap();
            CreateMap<PlatformType, DAL.App.DTO.PlatformType>().ReverseMap();
            CreateMap<Progress, DAL.App.DTO.Progress>().ReverseMap();
           
            CreateMap<AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
            CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
        }
    }
}
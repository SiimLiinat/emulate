using AutoMapper;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }
        public IAppRoleService AppRoles => GetService<IAppRoleService>(() => new AppRoleService(Uow, Uow.AppRoles, Mapper));
        public IAppUserService AppUsers => GetService<IAppUserService>(() => new AppUserService(Uow, Uow.AppUsers, Mapper));
        public ICompanyService Companies => GetService<ICompanyService>(() => new CompanyService(Uow, Uow.Companies, Mapper));
        public ICompatibilityService Compatibilities => GetService<ICompatibilityService>(() => new CompatibilityService(Uow, Uow.Compatibilities, Mapper));
        public ICompatibilityTypeService CompatibilityTypes => GetService<ICompatibilityTypeService>(() => new CompatibilityTypeService(Uow, Uow.CompatibilityTypes, Mapper));
        public IConfigurationService Configurations => GetService<IConfigurationService>(() => new ConfigurationService(Uow, Uow.Configurations, Mapper));
        public IEmulatorService Emulators => GetService<IEmulatorService>(() => new EmulatorService(Uow, Uow.Emulators, Mapper));
        public IGameService Games => GetService<IGameService>(() => new GameService(Uow, Uow.Games, Mapper));
        public IGameGenreService GameGenres => GetService<IGameGenreService>(() => new GameGenreService(Uow, Uow.GameGenres, Mapper));
        public IGameOnPlatformService GameOnPlatforms => GetService<IGameOnPlatformService>(() => new GameOnPlatformService(Uow, Uow.GameOnPlatforms, Mapper));
        public IGenreService Genres => GetService<IGenreService>(() => new GenreService(Uow, Uow.Genres, Mapper));
        public IMediaService Medias => GetService<IMediaService>(() => new MediaService(Uow, Uow.Medias, Mapper));
        public IMediaTypeService MediaTypes => GetService<IMediaTypeService>(() => new MediaTypeService(Uow, Uow.MediaTypes, Mapper));
        public IPlatformService Platforms => GetService<IPlatformService>(() => new PlatformService(Uow, Uow.Platforms, Mapper));
        public IPlatformTypeService PlatformTypes => GetService<IPlatformTypeService>(() => new PlatformTypeService(Uow, Uow.PlatformTypes, Mapper));
        public IProgressService Progresses => GetService<IProgressService>(() => new ProgressService(Uow, Uow.Progresses, Mapper));
    }
}
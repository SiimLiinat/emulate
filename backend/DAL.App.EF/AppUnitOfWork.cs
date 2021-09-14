using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }

        public IAppRoleRepository AppRoles => GetRepository(() => new AppRoleRepository(UowDbContext, Mapper));
        public IAppUserRepository AppUsers => GetRepository(() => new AppUserRepository(UowDbContext, Mapper));
        public ICompanyRepository Companies => GetRepository(() => new CompanyRepository(UowDbContext, Mapper));
        public ICompatibilityRepository Compatibilities => GetRepository(() => new CompatibilityRepository(UowDbContext, Mapper));
        public ICompatibilityTypeRepository CompatibilityTypes => GetRepository(() => new CompatibilityTypeRepository(UowDbContext, Mapper));
        public IConfigurationRepository Configurations => GetRepository(() => new ConfigurationRepository(UowDbContext, Mapper));
        public IEmulatorRepository Emulators => GetRepository(() => new EmulatorRepository(UowDbContext, Mapper));
        public IGameRepository Games => GetRepository(() => new GameRepository(UowDbContext, Mapper));
        public IGameGenreRepository GameGenres => GetRepository(() => new GameGenreRepository(UowDbContext, Mapper));
        public IGameOnPlatformRepository GameOnPlatforms => GetRepository(() => new GameOnPlatformRepository(UowDbContext, Mapper));
        public IGenreRepository Genres => GetRepository(() => new GenreRepository(UowDbContext, Mapper));
        public IMediaRepository Medias => GetRepository(() => new MediaRepository(UowDbContext, Mapper));
        public IMediaTypeRepository MediaTypes => GetRepository(() => new MediaTypeRepository(UowDbContext, Mapper));
        public IPlatformRepository Platforms => GetRepository(() => new PlatformRepository(UowDbContext, Mapper));
        public IPlatformTypeRepository PlatformTypes => GetRepository(() => new PlatformTypeRepository(UowDbContext, Mapper));
        public IProgressRepository Progresses => GetRepository(() => new ProgressRepository(UowDbContext, Mapper));
    }
}
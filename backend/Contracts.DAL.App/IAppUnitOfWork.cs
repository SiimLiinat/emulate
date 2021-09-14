using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IAppRoleRepository AppRoles { get;  }
        IAppUserRepository AppUsers { get;  }
        ICompanyRepository Companies { get;  }
        ICompatibilityRepository Compatibilities { get;  }
        ICompatibilityTypeRepository CompatibilityTypes { get;  }
        IConfigurationRepository Configurations { get;  }
        IEmulatorRepository Emulators { get;  }
        IGameRepository Games { get;  }
        IGameGenreRepository GameGenres { get;  }
        IGameOnPlatformRepository GameOnPlatforms { get;  }
        IGenreRepository Genres { get;  }
        IMediaRepository Medias { get;  }
        IMediaTypeRepository MediaTypes { get;  }
        IPlatformRepository Platforms { get;  }
        IPlatformTypeRepository PlatformTypes { get;  }
        IProgressRepository Progresses { get;  }
    }
}
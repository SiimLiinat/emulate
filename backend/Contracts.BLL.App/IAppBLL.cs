using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IAppRoleService AppRoles { get;  }
        IAppUserService AppUsers { get;  }
        ICompanyService Companies { get;  }
        ICompatibilityService Compatibilities { get;  }
        ICompatibilityTypeService CompatibilityTypes { get;  }
        IConfigurationService Configurations { get;  }
        IEmulatorService Emulators { get;  }
        IGameService Games { get;  }
        IGameGenreService GameGenres { get;  }
        IGameOnPlatformService GameOnPlatforms { get;  }
        IGenreService Genres { get;  }
        IMediaService Medias { get;  }
        IMediaTypeService MediaTypes { get;  }
        IPlatformService Platforms { get;  }
        IPlatformTypeService PlatformTypes { get;  }
        IProgressService Progresses { get;  }
    }
}
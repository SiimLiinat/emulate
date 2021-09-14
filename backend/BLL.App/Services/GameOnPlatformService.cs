using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class GameOnPlatformService : BaseEntityService<IAppUnitOfWork, IGameOnPlatformRepository, BllAppDTO.GameOnPlatform, DALAppDTO.GameOnPlatform>, IGameOnPlatformService
    {
        public GameOnPlatformService(IAppUnitOfWork serviceUow, IGameOnPlatformRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new GameOnPlatformMapper(mapper))
        {
        }
    }
}
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
    public class PlatformService : BaseEntityService<IAppUnitOfWork, IPlatformRepository, BllAppDTO.Platform, DALAppDTO.Platform>, IPlatformService
    {
        public PlatformService(IAppUnitOfWork serviceUow, IPlatformRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PlatformMapper(mapper))
        {
        }
    }
}
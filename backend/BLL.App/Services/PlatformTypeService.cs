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
    public class PlatformTypeService : BaseEntityService<IAppUnitOfWork, IPlatformTypeRepository, BllAppDTO.PlatformType, DALAppDTO.PlatformType>, IPlatformTypeService
    {
        public PlatformTypeService(IAppUnitOfWork serviceUow, IPlatformTypeRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PlatformTypeMapper(mapper))
        {
        }
    }
}
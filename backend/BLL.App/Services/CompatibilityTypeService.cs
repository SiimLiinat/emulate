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
    public class CompatibilityTypeService : BaseEntityService<IAppUnitOfWork, ICompatibilityTypeRepository, BllAppDTO.CompatibilityType, DALAppDTO.CompatibilityType>, ICompatibilityTypeService   
    {
        public CompatibilityTypeService(IAppUnitOfWork serviceUow, ICompatibilityTypeRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new CompatibilityTypeMapper(mapper))
        {
        }
    }
}
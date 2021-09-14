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
    public class AppRoleService : BaseEntityService<IAppUnitOfWork, IAppRoleRepository, BllAppDTO.Identity.AppRole, DALAppDTO.Identity.AppRole>, IAppRoleService
    {
        public AppRoleService(IAppUnitOfWork serviceUow, IAppRoleRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new AppRoleMapper(mapper))
        {
        }
    }
}
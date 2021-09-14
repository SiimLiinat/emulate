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
    public class AppUserService : BaseEntityService<IAppUnitOfWork, IAppUserRepository, BllAppDTO.Identity.AppUser, DALAppDTO.Identity.AppUser>, IAppUserService
    {
        public AppUserService(IAppUnitOfWork serviceUow, IAppUserRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new AppUserMapper(mapper))
        {
        }
    }
}
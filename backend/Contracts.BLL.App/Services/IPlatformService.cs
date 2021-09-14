using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IPlatformService : IBaseEntityService<BllAppDTO.Platform, DALAppDTO.Platform>, IPlatformRepositoryCustom<BllAppDTO.Platform>
    {
    }
}
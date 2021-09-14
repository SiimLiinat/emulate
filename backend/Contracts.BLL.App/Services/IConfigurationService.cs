using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IConfigurationService : IBaseEntityService<BllAppDTO.Configuration, DALAppDTO.Configuration>, IConfigurationRepositoryCustom<BllAppDTO.Configuration>
    {
        
    }
}
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BllAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface ICompanyService : IBaseEntityService<BllAppDTO.Company, DALAppDTO.Company>, ICompanyRepositoryCustom<BllAppDTO.Company>
    {
    }
}
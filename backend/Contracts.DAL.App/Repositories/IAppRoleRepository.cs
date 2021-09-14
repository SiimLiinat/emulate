using Contracts.DAL.Base.Repositories;
using DAL.App.DTO.Identity;

namespace Contracts.DAL.App.Repositories
{
    public interface IAppRoleRepository : IBaseRepository<AppRole>, IAppRoleRepositoryCustom<AppRole>
    {
        
    }
    
    public interface IAppRoleRepositoryCustom<TEntity>
    {
        
    }
}
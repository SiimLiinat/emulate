using Contracts.DAL.Base.Repositories;
using DAL.App.DTO.Identity;

namespace Contracts.DAL.App.Repositories
{
    public interface IAppUserRepository : IBaseRepository<AppUser>, IAppUserRepositoryCustom<AppUser>
    {
        
    }
    
    public interface IAppUserRepositoryCustom<TEntity>
    {
        
    }
}
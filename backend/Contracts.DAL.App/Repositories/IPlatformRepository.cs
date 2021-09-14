using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPlatformRepository : IBaseRepository<Platform>, IPlatformRepositoryCustom<Platform>
    {
        
    }
    
    public interface IPlatformRepositoryCustom<TEntity>
    {
    }
}
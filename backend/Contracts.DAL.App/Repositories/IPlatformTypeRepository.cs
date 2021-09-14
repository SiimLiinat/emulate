using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPlatformTypeRepository : IBaseRepository<PlatformType>, IPlatformTypeRepositoryCustom<PlatformType>
    {
        
    }
    
    public interface IPlatformTypeRepositoryCustom<TEntity>
    {
    }
}
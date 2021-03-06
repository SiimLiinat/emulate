using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICompatibilityRepository : IBaseRepository<Compatibility>, ICompatibilityRepositoryCustom<Compatibility>
    {
        
    }
    
    public interface ICompatibilityRepositoryCustom<TEntity>
    {
    }
}
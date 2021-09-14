using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICompatibilityTypeRepository : IBaseRepository<CompatibilityType>, ICompatibilityTypeRepositoryCustom<CompatibilityType>
    {
        
    }
    
    public interface ICompatibilityTypeRepositoryCustom<TEntity>
    {
        
    }
}
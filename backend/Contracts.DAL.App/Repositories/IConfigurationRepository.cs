using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IConfigurationRepository : IBaseRepository<Configuration>, IConfigurationRepositoryCustom<Configuration>
    {
        
    }
    
    public interface IConfigurationRepositoryCustom<TEntity>
    {
        
    }
}
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameRepository : IBaseRepository<Game>, IGameRepositoryCustom<Game>
    {
        
    }
    
    public interface IGameRepositoryCustom<TEntity>
    {
    }
}
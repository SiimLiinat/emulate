using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameOnPlatformRepository : IBaseRepository<GameOnPlatform>, IGameOnPlatformRepositoryCustom<GameOnPlatform>
    {
        
    }
    
    public interface IGameOnPlatformRepositoryCustom<TEntity>
    {
    }
}
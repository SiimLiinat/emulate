using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGameGenreRepository : IBaseRepository<GameGenre>, IGameGenreRepositoryCustom<GameGenre>
    {
        
    }
    
    public interface IGameGenreRepositoryCustom<TEntity>
    {
    }
}
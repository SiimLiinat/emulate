using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IMediaRepository : IBaseRepository<Media>, IMediaRepositoryCustom<Media>
    {
        
    }
    
    public interface IMediaRepositoryCustom<TEntity>
    {
    }
}
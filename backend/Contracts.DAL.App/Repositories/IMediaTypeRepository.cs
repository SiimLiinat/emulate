using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IMediaTypeRepository : IBaseRepository<MediaType>, IMediaTypeRepositoryCustom<MediaType>
    {
        
    }
    
    public interface IMediaTypeRepositoryCustom<TEntity>
    {
        
    }
}
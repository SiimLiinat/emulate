using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICompanyRepository : IBaseRepository<Company>, ICompanyRepositoryCustom<Company>
    {
        
    }
    
    // custom methods
    public interface ICompanyRepositoryCustom<TEntity>
    {
    }
}
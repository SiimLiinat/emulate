using System;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProgressRepository : IBaseRepository<Progress>, IProgressRepositoryCustom<Progress>
    {
        
    }
    
    public interface IProgressRepositoryCustom<TEntity>
    {
    }
}
using System;
using System.Collections.Generic;
using Contracts.Domain.Base;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepositorySync<TEntity, TKey> : IBaseRepositoryCommon<TEntity, TKey>
        where TEntity: class, IDomainEntityId<TKey>
        where TKey: IEquatable<TKey>
    {
        IEnumerable<TEntity> GetAll(TKey? userId, bool noTracking = true);
        TEntity FirstOrDefault(TKey id, TKey? userId, bool noTracking = true);
        TEntity Remove(TKey id, TKey? userId);
        bool Exists(TKey id, TKey? userId);
    }
}
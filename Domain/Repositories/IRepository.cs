using RiverLi.DDD.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RiverLi.DDD.Core.Domain.Repositories
{
    /// <summary>
    /// 通用仓储接口，定义完整的CRUD能力
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IRepository<TAggregateRoot, TKey> : IReadOnlyRepository<TAggregateRoot, TKey>
        where TAggregateRoot : class, IAggregateRoot, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取当前仓储所属的工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        Task<TAggregateRoot> AddAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TAggregateRoot> aggregateRoots, CancellationToken cancellationToken = default);
        
        Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(IEnumerable<TAggregateRoot> aggregateRoots, CancellationToken cancellationToken = default);
        
        Task DeleteAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
        Task DeleteByIdAsync(TKey id, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(IEnumerable<TAggregateRoot> aggregateRoots, CancellationToken cancellationToken = default);

        // 移除 SaveChangesAsync，统一由 UnitOfWork.SaveEntitiesAsync 管理
    }
}
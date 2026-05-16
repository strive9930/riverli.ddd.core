using RiverLi.DDD.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RiverLi.DDD.Core.Application.Common.Models;

namespace RiverLi.DDD.Core.Domain.Repositories
{
    /// <summary>
    /// 只读仓储接口，定义基础查询能力，适配CQS的读操作
    /// 仅对聚合根进行查询，泛型约束为聚合根+实体
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IReadOnlyRepository<TAggregateRoot, TKey>
        where TAggregateRoot : class, IAggregateRoot, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 根据主键获取聚合根（包含软删除过滤）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根实例，不存在则返回null</returns>
        Task<TAggregateRoot?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据主键列表批量获取聚合根
        /// </summary>
        /// <param name="ids">主键列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根列表</returns>
        Task<List<TAggregateRoot>> GetByIdsAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取所有聚合根（包含软删除过滤，慎用大数量场景）
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根列表</returns>
        Task<List<TAggregateRoot>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件查询聚合根列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根列表</returns>
        Task<List<TAggregateRoot>> FindAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件查询单个聚合根
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>聚合根实例，无匹配则返回null，多个匹配则抛出异常</returns>
        Task<TAggregateRoot?> SingleOrDefaultAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件判断是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>是否存在</returns>
        Task<bool> ExistsAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件统计数量
        /// </summary>
        /// <param name="predicate">查询条件（可选）</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>数量</returns>
        Task<long> CountAsync(Expression<Func<TAggregateRoot, bool>>? predicate = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取IQueryable查询对象，支持自定义查询（如分页、排序、联表）
        /// </summary>
        /// <returns>IQueryable查询对象</returns>
        IQueryable<TAggregateRoot> AsQueryable();
        
        // 新增分页查询方法
        Task<PagedResult<TAggregateRoot>> GetPagedAsync(
            PagedQuery query,
            Expression<Func<TAggregateRoot, bool>>? predicate = null,
            CancellationToken cancellationToken = default);
        
        // 新增排序支持
        Task<List<TAggregateRoot>> FindAsync(
            Expression<Func<TAggregateRoot, bool>> predicate,
            Expression<Func<TAggregateRoot, object>>? orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// 主键为Guid的只读仓储快捷接口
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
    public interface IReadOnlyRepository<TAggregateRoot> : IReadOnlyRepository<TAggregateRoot, Guid>
        where TAggregateRoot : class, IAggregateRoot, IEntity<Guid>
    {
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace RiverLi.DDD.Core.Domain.Repositories
{
    /// <summary>
    /// 工作单元接口
    /// 负责管理数据库事务的一致性，确保业务操作的原子性
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 提交所有变更（包含领域事件的分发与持久化）
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>操作是否成功</returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
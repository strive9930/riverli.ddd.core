using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverLi.DDD.Core.Domain.Events
{
    /// <summary>
    /// 领域事件发布者抽象接口
    /// 由基础设施层实现（如本地内存发布、RabbitMQ分布式发布）
    /// </summary>
    public interface IDomainEventPublisher
    {
        /// <summary>
        /// 发布单个领域事件
        /// </summary>
        /// <param name="domainEvent">领域事件实例</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>发布结果</returns>
        Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量发布领域事件
        /// </summary>
        /// <param name="domainEvents">领域事件集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>发布结果</returns>
        Task PublishBatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverLi.DDD.Core.Domain.Events
{
    /// <summary>
    /// 领域事件通用接口，所有领域事件必须实现此接口
    /// 集成MediatR.INotification，适配MediatR事件分发机制
    /// </summary>
    public interface IDomainEvent : INotification
    {
        /// <summary>
        /// 事件发生时间（UTC）
        /// </summary>
        DateTime EventTime { get; }

        /// <summary>
        /// 事件唯一标识（用于幂等处理）
        /// </summary>
        Guid EventId { get; }
        
    }
}

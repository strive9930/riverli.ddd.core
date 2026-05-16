using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverLi.DDD.Core.Domain.Common
{
    /// <summary>
    /// 聚合根标记接口（空接口）
    /// 界定DDD聚合边界，仓储仅对聚合根进行操作，聚合内实体通过聚合根管理
    /// </summary>
    public interface IAggregateRoot
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverLi.DDD.Core.Domain.Common
{
    /// <summary>
    /// 软删除标记接口，支持软删除的实体实现此接口
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// 删除时间（UTC），未删除时为null
        /// </summary>
        DateTime? DeleteTime { get; set; }
    }
}

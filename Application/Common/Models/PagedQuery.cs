using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverLi.DDD.Core.Application.Common.Models
{
    /// <summary>
    /// 通用分页查询模型
    /// 所有需要分页的查询DTO必须继承此类，统一分页参数规范
    /// </summary>
    public class PagedQuery
    {
        /// <summary>
        /// 当前页码（从1开始）
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 排序字段（如：CreateTime desc, Id asc）
        /// </summary>
        public string? SortField { get; set; }

        /// <summary>
        /// 校验分页参数的合法性，自动修正非法参数
        /// </summary>
        public virtual void ValidateAndCorrect()
        {
            if (PageIndex < 1) PageIndex = 1;
            if (PageSize < 1) PageSize = 10;
            if (PageSize > 100) PageSize = 100; // 限制最大页大小，防止大数据查询
        }
    }
}

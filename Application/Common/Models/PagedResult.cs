

namespace RiverLi.DDD.Core.Application.Common.Models
{
    /// <summary>
    /// 通用分页返回模型
    /// 所有分页查询的返回结果统一使用此类，包含数据、总条数、总页数等
    /// </summary>
    /// <typeparam name="T">数据项类型</typeparam>
    public class PagedResult<T>: IApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 数据总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数（自动计算）
        /// </summary>
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;

        /// <summary>
        /// 当前页数据列表
        /// </summary>
        public List<T> Data { get; set; } = new List<T>();
        
        public PagedResult()
        {
            Success = true;
            Message = "操作成功";
            Code = 200;
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            // Data 已在属性声明中初始化
        }
        
        /// <summary>
        /// 构建分页结果
        /// </summary>
        /// <param name="query">分页查询参数</param>
        /// <param name="totalCount">数据总条数</param>
        /// <param name="data">当前页数据</param>
        /// <returns>分页结果</returns>
        public static PagedResult<T> Create(PagedQuery query, int totalCount, IEnumerable<T> data)
        {
            query.ValidateAndCorrect();
            return new PagedResult<T>
            {
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                TotalCount = totalCount,
                Data = data.ToList()
            };
        }

        /// <summary>
        /// 构建空分页结果
        /// </summary>
        /// <param name="query">分页查询参数</param>
        /// <returns>空分页结果</returns>
        public static PagedResult<T> Empty(PagedQuery query)
        {
            query.ValidateAndCorrect();
            return new PagedResult<T>
            {
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 创建成功的分页响应结果
        /// 此方法会返回 PagedResult 实例，自动设置 Success=true、Message、Code、Timestamp
        /// </summary>
        /// <param name="data">当前页数据</param>
        /// <param name="totalCount">总条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="message">成功消息，默认“操作成功”</param>
        /// <param name="code">状态码，默认 200</param>
        /// <returns>PagedResult 实例</returns>
        public static PagedResult<T> SuccessResult(
            IEnumerable<T> data,
            int totalCount,
            int pageIndex = 1,
            int pageSize = 10,
            string message = "操作成功",
            int code = 200)
        {
            return new PagedResult<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = data.ToList(),
                Success = true,
                Message = message,
                Code = code,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }

        /// <summary>
        /// 创建失败的分页响应结果
        /// 此方法返回空的分页结果，并设置 Success=false、Message、Code
        /// </summary>
        /// <param name="message">错误消息，默认“操作失败”</param>
        /// <param name="code">错误状态码，默认 400</param>
        /// <param name="pageIndex">页码，默认 1</param>
        /// <param name="pageSize">每页大小，默认 10</param>
        /// <returns>空的分页结果</returns>
        public static PagedResult<T> FailResult(
            string message = "操作失败",
            int code = 400,
            int pageIndex = 1,
            int pageSize = 10)
        {
            return new PagedResult<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = 0,
                Data = new List<T>(),
                Success = false,
                Message = message,
                Code = code,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }
    }
}

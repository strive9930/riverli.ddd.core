namespace RiverLi.DDD.Core.Application.Common.Models;

    /// <summary>
    /// API结果接口
    /// </summary>
    public interface IApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        int Code { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        long Timestamp { get; set; }
    }

    /// <summary>
    /// 统一API响应结果（无数据）
    /// </summary>
    public class ApiResult : IApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 时间戳（Unix毫秒）
        /// </summary>
        public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        /// <summary>
        /// 错误详情（可选）
        /// </summary>
        public object? Errors { get; set; }

        /// <summary>
        /// 创建成功响应
        /// </summary>
        public static ApiResult SuccessResult(string message = "操作成功", int code = 200)
        {
            return new ApiResult
            {
                Success = true,
                Message = message,
                Code = code
            };
        }

        /// <summary>
        /// 创建失败响应
        /// </summary>
        public static ApiResult FailResult(string message = "操作失败", int code = 400)
        {
            return new ApiResult
            {
                Success = false,
                Message = message,
                Code = code
            };
        }
    }

    /// <summary>
    /// 统一API响应结果（带数据）
    /// </summary>
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// 响应数据
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 创建成功响应（带数据）
        /// </summary>
        public static ApiResult<T> SuccessResult(T data, string message = "操作成功", int code = 200)
        {
            return new ApiResult<T>
            {
                Success = true,
                Message = message,
                Code = code,
                Data = data
            };
        }

        /// <summary>
        /// 创建失败响应（带数据）
        /// </summary>
        public new static ApiResult<T> FailResult(string message = "操作失败", int code = 400)
        {
            return new ApiResult<T>
            {
                Success = false,
                Message = message,
                Code = code
            };
        }
        
        // 新增隐式转换
        public static implicit operator ApiResult<T>(T data)
            => SuccessResult(data);
        
        public static implicit operator ApiResult<T>(Result result)
            => result.Success 
                ? SuccessResult(default(T)!, result.Message) 
                : FailResult(result.Message,result.Code);
        
    }
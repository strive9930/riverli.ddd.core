using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverLi.DDD.Core.Application.Common.Models
{
    /// <summary>
    /// 非泛型全局统一返回结果模型
    /// 所有API接口统一返回格式，包含状态、消息、状态码
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 业务状态码（自定义，如200=成功，400=参数错误，500=业务异常）
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 提示消息（成功/失败描述）
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 扩展数据（可选，如错误详情、额外信息）
        /// </summary>
        public Dictionary<string, object>? Extensions { get; set; }

        /// <summary>
        /// 构建成功结果
        /// </summary>
        /// <param name="message">成功消息</param>
        /// <param name="code">成功状态码，默认200</param>
        /// <returns>成功结果</returns>
        public static Result SuccessResult(string message = "操作成功", int code = 200)
        {
            return new Result
            {
                Success = true,
                Code = code,
                Message = message
            };
        }

        /// <summary>
        /// 构建失败结果
        /// </summary>
        /// <param name="message">失败消息</param>
        /// <param name="code">失败状态码，默认500</param>
        /// <param name="extensions">扩展错误信息</param>
        /// <returns>失败结果</returns>
        public static Result FailResult(string message = "操作失败", int code = 500, Dictionary<string, object>? extensions = null)
        {
            return new Result
            {
                Success = false,
                Code = code,
                Message = message,
                Extensions = extensions
            };
        }

        /// <summary>
        /// 添加扩展数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>当前结果实例（链式调用）</returns>
        public Result AddExtension(string key, object value)
        {
            Extensions ??= new Dictionary<string, object>();
            if (!Extensions.ContainsKey(key))
                Extensions.Add(key, value);
            else
                Extensions[key] = value;
            return this;
        }
    }

    /// <summary>
    /// 泛型全局统一返回结果模型
    /// 包含数据体的返回结果，继承非泛型Result
    /// </summary>
    /// <typeparam name="T">数据体类型</typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// 业务数据体
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 构建带数据的成功结果
        /// </summary>
        /// <param name="data">业务数据</param>
        /// <param name="message">成功消息</param>
        /// <param name="code">成功状态码，默认200</param>
        /// <returns>成功结果</returns>
        public static Result<T> SuccessResult(T data, string message = "操作成功", int code = 200)
        {
            return new Result<T>
            {
                Success = true,
                Code = code,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// 构建带数据的失败结果
        /// </summary>
        /// <param name="message">失败消息</param>
        /// <param name="code">失败状态码，默认500</param>
        /// <param name="data">默认数据（可选）</param>
        /// <param name="extensions">扩展错误信息</param>
        /// <returns>失败结果</returns>
        public static Result<T> FailResult(string message = "操作失败", int code = 500, T? data = default, Dictionary<string, object>? extensions = null)
        {
            return new Result<T>
            {
                Success = false,
                Code = code,
                Message = message,
                Data = data,
                Extensions = extensions
            };
        }

        /// <summary>
        /// 从非泛型Result转换为泛型Result
        /// </summary>
        /// <param name="result">非泛型Result</param>
        /// <param name="data">泛型数据</param>
        /// <returns>泛型Result</returns>
        public static Result<T> FromResult(Result result, T? data = default)
        {
            return new Result<T>
            {
                Success = result.Success,
                Code = result.Code,
                Message = result.Message,
                Extensions = result.Extensions,
                Data = data
            };
        }
    }
}

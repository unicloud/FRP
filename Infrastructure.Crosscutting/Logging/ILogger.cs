#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 方案：FRP
// 项目：Infrastructure.Crosscutting
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Infrastructure.Crosscutting.Logging
{
    /// <summary>
    ///     日志的公共契约
    ///     <remarks>
    ///         可以有多种实现。
    ///     </remarks>
    /// </summary>
    public interface ILogger
    {
        #region 诊断

        /// <summary>
        ///     信息日志
        /// </summary>
        /// <param name="message">需要记录的信息</param>
        /// <param name="args">消息参数</param>
        void Info(string message, params object[] args);

        /// <summary>
        ///     调试日志
        /// </summary>
        /// <param name="message">调试消息</param>
        /// <param name="args">消息参数</param>
        void Debug(string message, params object[] args);

        /// <summary>
        ///     调试日志
        /// </summary>
        /// <param name="message">调试消息</param>
        /// <param name="exception">调试中的异常消息</param>
        /// <param name="args">消息参数</param>
        void Debug(string message, Exception exception, params object[] args);

        /// <summary>
        ///     调试日志
        /// </summary>
        /// <param name="item">包含调试消息的对象</param>
        void Debug(object item);

        /// <summary>
        ///     警告日志
        /// </summary>
        /// <param name="message">警告消息</param>
        /// <param name="args">消息参数</param>
        void Warning(string message, params object[] args);

        /// <summary>
        ///     错误日志
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="args">消息参数</param>
        void Error(string message, params object[] args);

        /// <summary>
        ///     错误日志
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="exception">错误相关的异常消息</param>
        /// <param name="args">消息参数</param>
        void Error(string message, Exception exception, params object[] args);

        /// <summary>
        ///     致命错误日志
        /// </summary>
        /// <param name="message">致命错误消息</param>
        /// <param name="args">消息参数</param>
        void Fatal(string message, params object[] args);

        /// <summary>
        ///     致命错误日志
        /// </summary>
        /// <param name="message">致命错误消息</param>
        /// <param name="exception">致命错误相关的异常消息</param>
        /// <param name="args">消息参数</param>
        void Fatal(string message, Exception exception, params object[] args);

        /// <summary>
        ///     异常处理日志
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="id">异常的ID</param>
        void ExceptionHandlerLoggedException(string message, Guid id);

        #endregion

        #region 审计

        /// <summary>
        ///     审计日志
        ///     非数据库操作
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="operation">操作</param>
        /// <param name="args">消息参数</param>
        void Audit(string user, string operation, params object[] args);

        /// <summary>
        ///     审计日志
        ///     数据库操作
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="dataBase">数据库</param>
        /// <param name="operation">操作</param>
        /// <param name="args">消息参数</param>
        void Audit(string user, string dataBase, string operation, params object[] args);

        #endregion

        #region 一般

        /// <summary>
        ///     开始处理
        /// </summary>
        /// <param name="process">处理流程</param>
        void Start(string process);

        /// <summary>
        ///     开始处理
        ///     数据库操作
        /// </summary>
        /// <param name="process">处理流程</param>
        /// <param name="dataBase">数据库</param>
        void Start(string process, string dataBase);

        /// <summary>
        ///     完成处理
        /// </summary>
        /// <param name="process">处理流程</param>
        void Finish(string process);

        /// <summary>
        ///     完成处理
        ///     数据库操作
        /// </summary>
        /// <param name="process">处理流程</param>
        /// <param name="dataBase">数据库</param>
        void Finish(string process, string dataBase);

        #endregion
    }
}
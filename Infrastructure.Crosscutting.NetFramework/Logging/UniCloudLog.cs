#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 方案：FRP
// 项目：Infrastructure.Crosscutting.NetFramework
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Diagnostics.Tracing;
using System.Globalization;
using UniCloud.Infrastructure.Crosscutting.Logging;

#endregion

namespace UniCloud.Infrastructure.Crosscutting.NetFramework.Logging
{
    [EventSource(Name = "UniCloud")]
    public class UniCloudLog : EventSource, ILogger
    {
        #region 构造单例

        private static readonly Lazy<UniCloudLog> Instance = new Lazy<UniCloudLog>(() => new UniCloudLog());

        private UniCloudLog()
        {
        }

        public static UniCloudLog Log
        {
            get { return Instance.Value; }
        }

        #endregion

        #region 嵌套类

        /// <summary>
        ///     关键字
        /// </summary>
        public static class Keywords
        {
            /// <summary>
            ///     用户界面
            /// </summary>
            public const EventKeywords UI = (EventKeywords) 1L;

            /// <summary>
            ///     数据库
            /// </summary>
            public const EventKeywords DataBase = (EventKeywords) 2L;

            /// <summary>
            ///     诊断
            /// </summary>
            public const EventKeywords Diagnostic = (EventKeywords) 4L;

            /// <summary>
            ///     性能
            /// </summary>
            public const EventKeywords Perf = (EventKeywords) 8L;

            /// <summary>
            ///     审计
            /// </summary>
            public const EventKeywords Audit = (EventKeywords) 16L;

            /// <summary>
            ///     一般
            /// </summary>
            public const EventKeywords General = (EventKeywords) 32L;
        }

        /// <summary>
        ///     操作码
        /// </summary>
        public static class Opcodes
        {
            /// <summary>
            ///     开始
            /// </summary>
            public const EventOpcode Start = (EventOpcode) 20;

            /// <summary>
            ///     完成
            /// </summary>
            public const EventOpcode Finish = (EventOpcode) 21;

            /// <summary>
            ///     错误
            /// </summary>
            public const EventOpcode Error = (EventOpcode) 22;

            /// <summary>
            ///     启动中
            /// </summary>
            public const EventOpcode Starting = (EventOpcode) 23;

            /// <summary>
            ///     跟踪
            /// </summary>
            public const EventOpcode Tracing = (EventOpcode) 24;

            /// <summary>
            ///     开始查询
            /// </summary>
            public const EventOpcode QueryStart = (EventOpcode) 30;

            /// <summary>
            ///     完成查询
            /// </summary>
            public const EventOpcode QueryFinish = (EventOpcode) 31;

            /// <summary>
            ///     查不到结果
            /// </summary>
            public const EventOpcode QueryNoResults = (EventOpcode) 32;

            /// <summary>
            ///     缓存查询
            /// </summary>
            public const EventOpcode CacheQuery = (EventOpcode) 40;

            /// <summary>
            ///     缓存更新
            /// </summary>
            public const EventOpcode CacheUpdate = (EventOpcode) 41;

            /// <summary>
            ///     快取命中
            /// </summary>
            public const EventOpcode CacheHit = (EventOpcode) 42;

            /// <summary>
            ///     高速缓存未中
            /// </summary>
            public const EventOpcode CacheMiss = (EventOpcode) 43;
        }

        /// <summary>
        ///     任务
        /// </summary>
        public static class Tasks
        {
            /// <summary>
            ///     用户
            /// </summary>
            public const EventTask User = (EventTask) 1;

            /// <summary>
            ///     页面
            /// </summary>
            public const EventTask Page = (EventTask) 2;

            /// <summary>
            ///     处理流程
            /// </summary>
            public const EventTask Process = (EventTask) 3;

            /// <summary>
            ///     查询
            /// </summary>
            public const EventTask Query = (EventTask) 4;

            /// <summary>
            ///     缓存
            /// </summary>
            public const EventTask Cache = (EventTask) 5;

            /// <summary>
            ///     初始化
            /// </summary>
            public const EventTask Initialize = (EventTask) 6;

            /// <summary>
            ///     跟踪
            /// </summary>
            public const EventTask Tracing = (EventTask) 7;
        }

        #endregion

        #region ILogger 成员

        #region 诊断

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="message">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="args"></param>
        [Event(100,
            Level = EventLevel.Informational,
            Keywords = Keywords.Diagnostic,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Tracing,
            Version = 1)]
        public void Info(string message, params object[] args)
        {
            if (IsEnabled())
            {
                var msg = string.Format(CultureInfo.InvariantCulture, message, args);
                WriteEvent(100, msg);
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="message">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="args"></param>
        [Event(110,
            Level = EventLevel.Informational,
            Keywords = Keywords.Diagnostic,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Tracing,
            Version = 1)]
        public void Debug(string message, params object[] args)
        {
            if (IsEnabled())
            {
                var msg = string.Format(CultureInfo.InvariantCulture, message, args);
                WriteEvent(110, msg);
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="message">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="exception">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="args"></param>
        [Event(111,
            Level = EventLevel.Informational,
            Keywords = Keywords.Diagnostic,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Tracing,
            Version = 1,
            Message = "{0}，{1}")]
        public void Debug(string message, Exception exception, params object[] args)
        {
            if (IsEnabled())
            {
                var msg = string.Format(CultureInfo.InvariantCulture, message, args);
                WriteEvent(111, msg, exception.ToString());
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="item">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        [Event(112,
            Level = EventLevel.Informational,
            Keywords = Keywords.Diagnostic,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Tracing,
            Version = 1)]
        public void Debug(object item)
        {
            if (IsEnabled())
            {
                WriteEvent(112, item.ToString());
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="message">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="args"></param>
        [Event(120,
            Level = EventLevel.Warning,
            Keywords = Keywords.Diagnostic,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Error,
            Version = 1)]
        public void Warning(string message, params object[] args)
        {
            if (IsEnabled())
            {
                var msg = string.Format(CultureInfo.InvariantCulture, message, args);
                WriteEvent(120, msg);
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="message">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="args"></param>
        [Event(130,
            Level = EventLevel.Error,
            Keywords = Keywords.Diagnostic,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Error,
            Version = 1)]
        public void Error(string message, params object[] args)
        {
            if (IsEnabled())
            {
                var msg = string.Format(CultureInfo.InvariantCulture, message, args);
                WriteEvent(130, msg);
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="message">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="exception">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="args"></param>
        [Event(131,
            Level = EventLevel.Error,
            Keywords = Keywords.Diagnostic,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Error,
            Version = 1,
            Message = "{0}，{1}")]
        public void Error(string message, Exception exception, params object[] args)
        {
            if (IsEnabled())
            {
                var msg = string.Format(CultureInfo.InvariantCulture, message, args);
                WriteEvent(131, msg, exception.ToString());
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="message">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="args"></param>
        [Event(140,
            Level = EventLevel.Critical,
            Keywords = Keywords.Diagnostic,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Error,
            Version = 1)]
        public void Fatal(string message, params object[] args)
        {
            if (IsEnabled())
            {
                var msg = string.Format(CultureInfo.InvariantCulture, message, args);
                WriteEvent(140, msg);
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="message">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="exception">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="args"></param>
        [Event(141,
            Level = EventLevel.Critical,
            Keywords = Keywords.Diagnostic,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Error,
            Version = 1,
            Message = "{0}，{1}")]
        public void Fatal(string message, Exception exception, params object[] args)
        {
            if (IsEnabled())
            {
                var msg = string.Format(CultureInfo.InvariantCulture, message, args);
                WriteEvent(141, msg, exception.ToString());
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="message">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="id">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        [Event(150,
            Level = EventLevel.LogAlways,
            Keywords = Keywords.Diagnostic,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Error,
            Version = 1,
            Message = "{0}，异常的ID为：{1}")]
        public void ExceptionHandlerLoggedException(string message, Guid id)
        {
            if (IsEnabled())
            {
                WriteEvent(150, message, id.ToString());
            }
        }

        #endregion

        #region 审计

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="user">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="operation">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="args"></param>
        [Event(200,
            Level = EventLevel.LogAlways,
            Keywords = Keywords.Audit,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Tracing,
            Version = 1,
            Message = "用户【{0}】{1}")]
        public void Audit(string user, string operation, params object[] args)
        {
            if (IsEnabled())
            {
                var msg = string.Format(CultureInfo.InvariantCulture, operation, args);
                WriteEvent(200, user, msg);
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="user">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="dataBase">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="operation">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="args">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        [Event(201,
            Level = EventLevel.LogAlways,
            Keywords = Keywords.Audit | Keywords.DataBase,
            Task = Tasks.Tracing,
            Opcode = Opcodes.Tracing,
            Version = 1,
            Message = "用户【{0}】针对数据库【{1}】{2}")]
        public void Audit(string user, string dataBase, string operation, params object[] args)
        {
            if (IsEnabled())
            {
                WriteEvent(201, user, dataBase, operation);
            }
        }

        #endregion

        #region 一般

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="process">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        [Event(300,
            Level = EventLevel.Informational,
            Keywords = Keywords.General,
            Task = Tasks.Process,
            Opcode = Opcodes.Start,
            Version = 1,
            Message = "{0}开始")]
        public void Start(string process)
        {
            if (IsEnabled())
            {
                WriteEvent(300, process);
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="process">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="dataBase">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        [Event(301,
            Level = EventLevel.Informational,
            Keywords = Keywords.General | Keywords.DataBase,
            Task = Tasks.Process,
            Opcode = Opcodes.Start,
            Version = 1,
            Message = "数据库【{1}】{0}开始")]
        public void Start(string process, string dataBase)
        {
            if (IsEnabled())
            {
                WriteEvent(301, process, dataBase);
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="process">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        [Event(302,
            Level = EventLevel.Informational,
            Keywords = Keywords.General,
            Task = Tasks.Process,
            Opcode = Opcodes.Finish,
            Version = 1,
            Message = "{0}完成")]
        public void Finish(string process)
        {
            if (IsEnabled())
            {
                WriteEvent(302, process);
            }
        }

        /// <summary>
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </summary>
        /// <param name="process">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        /// <param name="dataBase">
        ///     <see cref="Crosscutting.Logging.ILogger" />
        /// </param>
        [Event(303,
            Level = EventLevel.Informational,
            Keywords = Keywords.General | Keywords.DataBase,
            Task = Tasks.Process,
            Opcode = Opcodes.Finish,
            Version = 1,
            Message = "数据库【{1}】{0}完成")]
        public void Finish(string process, string dataBase)
        {
            if (IsEnabled())
            {
                WriteEvent(303, process, dataBase);
            }
        }

        #endregion

        #endregion
    }
}
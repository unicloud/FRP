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

namespace UniCloud.Infrastructure.Crosscutting.Logging
{
    /// <summary>
    ///     日志工厂
    /// </summary>
    public static class LoggerFactory
    {
        #region Members

        private static ILoggerFactory _currentLogFactory;

        #endregion

        #region Public Methods

        /// <summary>
        ///     从日志工厂获取的日志记录器
        /// </summary>
        public static ILogger Log
        {
            get { return (_currentLogFactory != null) ? _currentLogFactory.Create() : null; }
        }

        /// <summary>
        ///     设置当前的日志工厂
        /// </summary>
        /// <param name="logFactory">使用的日志工厂</param>
        public static void SetCurrent(ILoggerFactory logFactory)
        {
            _currentLogFactory = logFactory;
        }

        #endregion
    }
}
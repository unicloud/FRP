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

using UniCloud.Infrastructure.Crosscutting.Logging;

#endregion

namespace UniCloud.Infrastructure.Crosscutting.NetFramework.Logging
{
    /// <summary>
    ///     日志工厂实现
    /// </summary>
    public class UniCloudLogFactory : ILoggerFactory
    {
        #region ILoggerFactory 成员

        /// <summary>
        ///     创建基于语义日志的日志记录器
        /// </summary>
        /// <returns>ILogger接口的实现实例</returns>
        public ILogger Create()
        {
            return UniCloudLog.Log;
        }

        #endregion
    }
}
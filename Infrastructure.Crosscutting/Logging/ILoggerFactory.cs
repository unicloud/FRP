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
    ///     日志抽象工厂接口
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        ///     创建新的日志记录器
        /// </summary>
        /// <returns>创建的日志记录器</returns>
        ILogger Create();
    }
}
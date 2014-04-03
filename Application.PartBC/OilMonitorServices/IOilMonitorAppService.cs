#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/23，14:18
// 方案：FRP
// 项目：Application.PartBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;

#endregion

namespace UniCloud.Application.PartBC.OilMonitorServices
{
    /// <summary>
    ///     滑油监控应用服务接口
    /// </summary>
    public interface IOilMonitorAppService
    {
        /// <summary>
        ///     获取发动机滑油数据
        /// </summary>
        /// <returns>发动机滑油数据集合</returns>
        IQueryable<EngineOilDTO> GetEngineOils();

        /// <summary>
        ///     获取APU滑油数据
        /// </summary>
        /// <returns>APU滑油数据集合</returns>
        IQueryable<APUOilDTO> GetAPUOils();

        /// <summary>
        ///     获取滑油消耗数据
        /// </summary>
        /// <returns>滑油消耗数据集合</returns>
        IQueryable<OilMonitorDTO> GetOilMonitors();
    }
}
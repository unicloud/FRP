#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/03/13，15:03
// 文件名：IAircraftConfigurationAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftConfigurationServices
{
    /// <summary>
    ///     飞机配置服务接口。
    /// </summary>
    public interface IAircraftConfigurationAppService
    {
        /// <summary>
        ///     获取所有飞机配置
        /// </summary>
        /// <returns></returns>
        IQueryable<AircraftConfigurationDTO> GetAircraftConfigurations();
    }
}
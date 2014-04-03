#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：IAircraftTypeAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;

#endregion

namespace UniCloud.Application.AircraftConfigBC.AircraftTypeServices
{
    /// <summary>
    ///     机型服务接口。
    /// </summary>
    public interface IAircraftTypeAppService
    {
        /// <summary>
        ///     获取所有机型
        /// </summary>
        /// <returns></returns>
        IQueryable<AircraftTypeDTO> GetAircraftTypes();

        /// <summary>
        ///     获取所有民航机型
        /// </summary>
        /// <returns></returns>
        IQueryable<CAACAircraftTypeDTO> GetCAACAircraftTypes();
    }
}
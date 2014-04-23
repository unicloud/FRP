#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/22，17:04
// 文件名：IIssuedUnitAppService.cs
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

namespace UniCloud.Application.FleetPlanBC.IssuedUnitServices
{
    /// <summary>
    ///     发文单位服务接口。
    /// </summary>
    public interface IIssuedUnitAppService
    {
        /// <summary>
        ///     获取所有发文单位
        /// </summary>
        /// <returns></returns>
        IQueryable<IssuedUnitDTO> GetIssuedUnits();
    }
}
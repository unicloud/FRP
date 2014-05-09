#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 11:52:18
// 文件名：IAnnualMaintainPlanAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 11:52:18
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;

#endregion

namespace UniCloud.Application.PartBC.AnnualMaintainPlanServices
{
    /// <summary>
    /// AnnualMaintainPlan的服务接口。
    /// </summary>
    public interface IAnnualMaintainPlanAppService
    {
        /// <summary>
        /// 获取所有EngineMaintainPlan。
        /// </summary>
        IQueryable<EngineMaintainPlanDTO> GetEngineMaintainPlans();

        /// <summary>
        /// 获取所有AircraftMaintainPlan。
        /// </summary>
        IQueryable<AircraftMaintainPlanDTO> GetAircraftMaintainPlans();
    }
}

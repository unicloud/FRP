#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 9:46:13
// 文件名：IAnnualMaintainPlanRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 9:46:13
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.PartBC.Aggregates.AnnualMaintainPlanAgg
{
    /// <summary>
    /// AnnualMaintainPlan仓储接口。
    /// </summary>
    public partial interface IAnnualMaintainPlanRepository : IRepository<AnnualMaintainPlan>
    {
        #region EngineMaintainPlan
        EngineMaintainPlan GetEngineMaintainPlan(int id);
        void DeleteEngineMaintainPlan(EngineMaintainPlan engineMaintainPlan);
        void RemoveEngineMaintainPlanDetail(EngineMaintainPlanDetail engineMaintainPlanDetail);
        #endregion
    }
}

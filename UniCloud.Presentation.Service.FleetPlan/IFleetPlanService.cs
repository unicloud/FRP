#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 14:17:06
// 文件名：IFleetPlanService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 14:17:06
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.Service.FleetPlan
{
    public interface IFleetPlanService : IService
    {
        /// <summary>
        ///     数据服务上下文
        /// </summary>
        FleetPlanData Context { get; }
    }
}
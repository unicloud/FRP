#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 14:17:13
// 文件名：FleetPlanService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 14:17:13
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Client;

#endregion

namespace UniCloud.Presentation.Service.FleetPlan
{
    public class FleetPlanService : ServiceBase, IFleetPlanService
    {
        public FleetPlanService(DataServiceContext context)
            : base(context)
        {
        }
    }
}

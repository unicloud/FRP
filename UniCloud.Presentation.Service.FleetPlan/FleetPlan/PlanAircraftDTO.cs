#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/6 15:53:45
// 文件名：PlanAircraftDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan
{
    public partial class PlanAircraftDTO
    {
        #region 属性

        /// <summary>
        ///     计划飞机管理状态
        /// </summary>
        public ManageStatus ManaStatus
        {
            get { return (ManageStatus)Status; }
        }

        #endregion
    }
}

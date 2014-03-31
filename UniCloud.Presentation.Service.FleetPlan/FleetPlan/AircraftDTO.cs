#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/10/16 16:09:35
// 文件名：AircraftDTO
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
    public partial class AircraftDTO
    {
        partial void OnExportDateChanged()
        {
            if (ExportDate == null)
                OperateStatus = "运营中";
            else OperateStatus = "已退出";
        }
    }
}

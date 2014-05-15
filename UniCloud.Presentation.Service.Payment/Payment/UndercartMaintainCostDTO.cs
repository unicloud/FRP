#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 17:10:34
// 文件名：UndercartMaintainCostDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 17:10:34
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Presentation.Service.Payment.Payment
{
    public partial class UndercartMaintainCostDTO
    {
        partial void OnInMaintainTimeChanged()
        {
            TotalDays = (OutMaintainTime.Date - InMaintainTime.Date).Days + 1;
        }

        partial void OnOutMaintainTimeChanged()
        {
            TotalDays = (OutMaintainTime.Date - InMaintainTime.Date).Days + 1;
        }

        partial void OnAcutalOutMaintainTimeChanged()
        {
            AcutalTotalDays = (AcutalOutMaintainTime.Date - AcutalInMaintainTime.Date).Days + 1;
        }

        partial void OnAcutalInMaintainTimeChanged()
        {
            AcutalTotalDays = (AcutalOutMaintainTime.Date - AcutalInMaintainTime.Date).Days + 1;
        }


    }
}

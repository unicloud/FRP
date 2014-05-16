#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 15:33:56
// 文件名：RegularCheckMaintainCostDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 15:33:56
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Presentation.Service.Payment.Payment
{
    public partial class RegularCheckMaintainCostDTO
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

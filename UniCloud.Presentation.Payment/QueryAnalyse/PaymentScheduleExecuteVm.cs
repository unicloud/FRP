#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/27 15:02:13
// 文件名：PaymentScheduleExecuteVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/27 15:02:13
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.QueryAnalyse
{
    [Export(typeof (PaymentScheduleExecuteVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PaymentScheduleExecuteVm
    {
    }
}
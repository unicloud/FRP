#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/18，17:12
// 文件名：StandardPaymentScheduleDTO.cs
// 程序集：UniCloud.Presentation.Service.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using UniCloud.Presentation.Service.Payment.Payment.Enums;

namespace UniCloud.Presentation.Service.Payment.Payment
{
    public partial class StandardPaymentScheduleDTO
    {
        /// <summary>
        /// 完成的PaymentScheduleStatus类型状态
        /// </summary>
        public PaymentScheduleStatus CompletedString
        {
            get
            {
                return IsCompleted ? PaymentScheduleStatus.已完成 : PaymentScheduleStatus.未完成;
            }
        }
    }
}

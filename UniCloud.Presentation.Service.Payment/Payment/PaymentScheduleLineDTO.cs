#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/17，17:12
// 文件名：AcPaymentScheduleDTO.cs
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
    public partial class PaymentScheduleLineDTO
    {
        /// <summary>
        ///     付款计划行状态
        /// </summary>
        public ControlStatus ControlStatus
        {
            get { return (ControlStatus)Status; }
        }

        /// <summary>
        /// 当发票不空时，不可编辑
        /// </summary>
        public bool CanEdit
        {
            get
            {
                return InvoiceId != null;
            }
        }
    }
}

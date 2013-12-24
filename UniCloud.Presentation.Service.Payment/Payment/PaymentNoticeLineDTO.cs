#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/20 11:59:52
// 文件名：PaymentNoticeLineDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/20 11:59:52
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Service.Payment.Payment
{
    public partial class PaymentNoticeLineDTO
    {
        partial void OnInvoiceTypeStringChanging(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("请选择发票类型");
            }
            InvoiceType = (int)(InvoiceType)Enum.Parse(typeof(InvoiceType), value, true);
        }

    }
}

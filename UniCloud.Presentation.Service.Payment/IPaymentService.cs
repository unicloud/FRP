﻿#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13 14:42:35
// 文件名：IPaymentService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/13 14:42:35
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Service.Payment
{
    public interface IPaymentService : IService
    {
        /// <summary>
        ///     数据服务上下文
        /// </summary>
        PaymentData Context { get; }
    }
}
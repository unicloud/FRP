﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，10:08
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

namespace UniCloud.Domain.PaymentBC.Enums
{
    /// <summary>
    ///     发票类型
    /// </summary>
    public enum InvoiceType
    {
        采购发票 = 0,
        预付款发票 = 1,
        租赁发票 = 2,
        维修发票 = 3,
        贷项单 = 4
    }
}
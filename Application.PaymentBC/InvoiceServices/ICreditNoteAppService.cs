﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 11:05:02
// 文件名：ICreditNoteAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.PaymentBC.DTO;

#endregion

namespace UniCloud.Application.PaymentBC.InvoiceServices
{
    /// <summary>
    /// 贷项单服务接口
    /// </summary>
    public interface ICreditNoteAppService
    {
        /// <summary>
        ///     获取所有贷项单
        /// </summary>
        /// <returns></returns>
        IQueryable<CreditNoteDTO> GetCreditNoteInvoices();
    }
}

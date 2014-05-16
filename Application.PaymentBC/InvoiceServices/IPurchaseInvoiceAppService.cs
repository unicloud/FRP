#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 11:03:54
// 文件名：IPurchaseInvoiceAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;

#endregion

namespace UniCloud.Application.PaymentBC.InvoiceServices
{
    /// <summary>
    ///     采购发票服务接口
    /// </summary>
    public interface IPurchaseInvoiceAppService
    {
        /// <summary>
        ///     获取所有采购发票
        /// </summary>
        /// <returns></returns>
        IQueryable<PurchaseInvoiceDTO> GetPurchaseInvoices();

        /// <summary>
        ///     获取所有杂项发票
        /// </summary>
        /// <returns></returns>
        IQueryable<SundryInvoiceDTO> GetSundryInvoices();

    }
}
#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 11:15:56
// 文件名：IMaintainInvoiceAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 11:15:56
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;

#endregion

namespace UniCloud.Application.PaymentBC.MaintainInvoiceServices
{
    /// <summary>
    ///     表示用于维修发票相关信息服务
    /// </summary>
    public interface IMaintainInvoiceAppService
    {
        /// <summary>
        ///     获取所有维修发票
        /// </summary>
        /// <returns>所有维修发票</returns>
        IQueryable<BaseMaintainInvoiceDTO> GetMaintainInvoices();

        /// <summary>
        ///     获取所有发动机维修发票
        /// </summary>
        /// <returns>所有发动机维修发票</returns>
        IQueryable<EngineMaintainInvoiceDTO> GetEngineMaintainInvoices();

        /// <summary>
        ///     获取所有APU维修发票
        /// </summary>
        /// <returns>所有APU维修发票</returns>
        IQueryable<APUMaintainInvoiceDTO> GetApuMaintainInvoices();

        /// <summary>
        ///     获取所有机身维修发票
        /// </summary>
        /// <returns>所有机身维修发票</returns>
        IQueryable<AirframeMaintainInvoiceDTO> GetAirframeMaintainInvoices();

        /// <summary>
        /// 获取所有起落架维修发票
        /// </summary>
        /// <returns>所有起落架维修发票</returns>
        IQueryable<UndercartMaintainInvoiceDTO> GetUndercartMaintainInvoices();
    }
}

#region Version Info

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

using System;
using Telerik.Windows.Data;
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

        #region 获取静态数据

        /// <summary>
        ///     获取供应商
        /// </summary>
        /// <param name="loaded">回调</param>
        /// <param name="forceLoad">是否强制加载</param>
        /// <returns>供应商集合</returns>
        QueryableDataServiceCollectionView<SupplierDTO> GetSupplier(Action loaded, bool forceLoad = false);

        /// <summary>
        ///     获取币种
        /// </summary>
        /// <param name="loaded">回调</param>
        /// <param name="forceLoad">是否强制加载</param>
        /// <returns>币种集合</returns>
        QueryableDataServiceCollectionView<CurrencyDTO> GetCurrency(Action loaded, bool forceLoad = false);

        #endregion
    }
}
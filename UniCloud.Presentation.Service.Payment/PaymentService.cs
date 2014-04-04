#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13 14:42:20
// 文件名：PaymentService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/13 14:42:20
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using Telerik.Windows.Data;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Service.Payment
{
    [Export(typeof (IPaymentService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PaymentService : ServiceBase, IPaymentService
    {
        public PaymentService()
        {
            context = new PaymentData(AgentHelper.PaymentUri);
        }

        #region IPaymentService 成员

        public PaymentData Context
        {
            get { return context as PaymentData; }
        }

        #region 获取静态数据

        public QueryableDataServiceCollectionView<SupplierDTO> GetSupplier(Action loaded, bool forceLoad = false)
        {
            return GetStaticData(Context.Suppliers, loaded, forceLoad);
        }

        public QueryableDataServiceCollectionView<CurrencyDTO> GetCurrency(Action loaded, bool forceLoad = false)
        {
            return GetStaticData(Context.Currencies, loaded, forceLoad);
        }

        #endregion

        #endregion
    }
}
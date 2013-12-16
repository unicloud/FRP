#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 11:03:44
// 文件名：PurchaseInvoiceAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.InvoiceQueries;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;

#endregion

namespace UniCloud.Application.PaymentBC.InvoiceServices
{
    /// <summary>
    ///     采购发票服务实现
    /// </summary>
    public class PurchaseInvoiceAppService : IPurchaseInvoiceAppService
    {
        private readonly IPurchaseInvoiceQuery _purchaseInvoiceQuery;
        private readonly IInvoiceRepository _invoiceRepository;

        public PurchaseInvoiceAppService(IPurchaseInvoiceQuery purchaseInvoiceQuery,
            IInvoiceRepository invoiceRepository)
        {
            _purchaseInvoiceQuery = purchaseInvoiceQuery;
            _invoiceRepository = invoiceRepository;
        }

        #region PurchaseInvoiceDTO

        /// <summary>
        ///     获取所有采购发票
        /// </summary>
        /// <returns></returns>
        public IQueryable<PurchaseInvoiceDTO> GetPurchaseInvoices()
        {
            var queryBuilder =
                new QueryBuilder<PurchaseInvoice>();
            return _purchaseInvoiceQuery.PurchaseInvoiceDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增采购发票。
        /// </summary>
        /// <param name="purchaseInvoice">采购发票DTO。</param>
        [Insert(typeof(PurchaseInvoiceDTO))]
        public void InsertPurchaseInvoice(PurchaseInvoiceDTO purchaseInvoice)
        {

        }

        /// <summary>
        ///     更新采购发票。
        /// </summary>
        /// <param name="purchaseInvoice">采购发票DTO。</param>
        [Update(typeof(PurchaseInvoiceDTO))]
        public void ModifyPurchaseInvoice(PurchaseInvoiceDTO purchaseInvoice)
        {
            var updatePurchaseInvoice = _invoiceRepository.GetFiltered(t => t.Id == purchaseInvoice.PurchaseInvoiceId).FirstOrDefault();
            //获取需要更新的对象。
            if (updatePurchaseInvoice != null)
            {
                //更新主表。


                //更新从表。
            }
            _invoiceRepository.Modify(updatePurchaseInvoice);
        }

        /// <summary>
        ///     删除采购发票。
        /// </summary>
        /// <param name="purchaseInvoice">采购发票DTO。</param>
        [Delete(typeof(PurchaseInvoiceDTO))]
        public void DeletePurchaseInvoice(PurchaseInvoiceDTO purchaseInvoice)
        {
            if (purchaseInvoice == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delPurchaseInvoice = _invoiceRepository.Get(purchaseInvoice.PurchaseInvoiceId);
            //获取需要删除的对象。
            if (delPurchaseInvoice != null)
            {
                _invoiceRepository.DeleteInvoice(delPurchaseInvoice);//删除采购发票。
            }
        }

        #endregion
    }
}

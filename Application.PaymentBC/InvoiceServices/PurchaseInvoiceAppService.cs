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
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.InvoiceQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Application.PaymentBC.InvoiceServices
{
    /// <summary>
    ///     采购发票服务实现
    /// </summary>
   [LogAOP]
    public class PurchaseInvoiceAppService : ContextBoundObject, IPurchaseInvoiceAppService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPurchaseInvoiceQuery _purchaseInvoiceQuery;
        private readonly ISupplierRepository _supplierRepository;

        public PurchaseInvoiceAppService(IPurchaseInvoiceQuery purchaseInvoiceQuery,
            IInvoiceRepository invoiceRepository,
            ISupplierRepository supplierRepository,
            IOrderRepository orderRepository,
            ICurrencyRepository currencyRepository)
        {
            _purchaseInvoiceQuery = purchaseInvoiceQuery;
            _invoiceRepository = invoiceRepository;
            _supplierRepository = supplierRepository;
            _orderRepository = orderRepository;
            _currencyRepository = currencyRepository;
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
        [Insert(typeof (PurchaseInvoiceDTO))]
        public void InsertPurchaseInvoice(PurchaseInvoiceDTO purchaseInvoice)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.Id == purchaseInvoice.SupplierId).FirstOrDefault();
            var order = _orderRepository.Get(purchaseInvoice.OrderId);
            var currency = _currencyRepository.GetFiltered(p => p.Id == purchaseInvoice.CurrencyId).FirstOrDefault();

            var newPurchaseInvoice = InvoiceFactory.CreatePurchaseInvoice(purchaseInvoice.InvoideCode,
                purchaseInvoice.InvoiceDate, purchaseInvoice.OperatorName);
            var date = DateTime.Now.Date;
            var seq = _invoiceRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            newPurchaseInvoice.SetInvoiceNumber(seq);
            newPurchaseInvoice.SetSupplier(supplier);
            newPurchaseInvoice.SetOrder(order);
            newPurchaseInvoice.SetPaidAmount(purchaseInvoice.PaidAmount);
            newPurchaseInvoice.SetCurrency(currency);
            newPurchaseInvoice.SetPaymentScheduleLine(purchaseInvoice.PaymentScheduleLineId);
            newPurchaseInvoice.SetInvoiceStatus(InvoiceStatus.草稿);
            foreach (var invoiceLine in purchaseInvoice.InvoiceLines)
            {
                if (order != null)
                {
                    var orderLine = order.OrderLines.FirstOrDefault(p => p.Id == invoiceLine.OrderLineId);
                    newPurchaseInvoice.AddInvoiceLine(invoiceLine.Amount, orderLine, invoiceLine.Note);
                }
                else
                {
                    newPurchaseInvoice.AddInvoiceLine( invoiceLine.Amount, null, invoiceLine.Note);
                }
            }
            newPurchaseInvoice.SetInvoiceValue();
            _invoiceRepository.Add(newPurchaseInvoice);
        }

        /// <summary>
        ///     更新采购发票。
        /// </summary>
        /// <param name="purchaseInvoice">采购发票DTO。</param>
        [Update(typeof (PurchaseInvoiceDTO))]
        public void ModifyPurchaseInvoice(PurchaseInvoiceDTO purchaseInvoice)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.Id == purchaseInvoice.SupplierId).FirstOrDefault();
            var order = _orderRepository.Get(purchaseInvoice.OrderId);
            var currency = _currencyRepository.GetFiltered(p => p.Id == purchaseInvoice.CurrencyId).FirstOrDefault();

            var updatePurchaseInvoice = _invoiceRepository.GetBasePurchaseInvoice(purchaseInvoice.PurchaseInvoiceId);
            //获取需要更新的对象。
            if (updatePurchaseInvoice != null)
            {
                InvoiceFactory.SetInvoice(updatePurchaseInvoice, purchaseInvoice.InvoideCode,
                    purchaseInvoice.InvoiceDate, purchaseInvoice.OperatorName, purchaseInvoice.InvoiceNumber, supplier,
                    order,
                    purchaseInvoice.PaidAmount, currency, purchaseInvoice.PaymentScheduleLineId, purchaseInvoice.Status);
                //更新主表。

                UpdateInvoiceLines(purchaseInvoice.InvoiceLines, updatePurchaseInvoice, order);
                //更新从表。
            }
            _invoiceRepository.Modify(updatePurchaseInvoice);
        }

        /// <summary>
        ///     删除采购发票。
        /// </summary>
        /// <param name="purchaseInvoice">采购发票DTO。</param>
        [Delete(typeof (PurchaseInvoiceDTO))]
        public void DeletePurchaseInvoice(PurchaseInvoiceDTO purchaseInvoice)
        {
            if (purchaseInvoice == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delPurchaseInvoice = _invoiceRepository.GetBasePurchaseInvoice(purchaseInvoice.PurchaseInvoiceId);
            //获取需要删除的对象。
            if (delPurchaseInvoice != null)
            {
                _invoiceRepository.DeleteInvoice(delPurchaseInvoice); //删除采购发票。
            }
        }

        #endregion

        #region 更新发票行集合

        /// <summary>
        ///     更新发票行集合
        /// </summary>
        /// <param name="sourceInvoiceLines">客户端集合</param>
        /// <param name="dstInvoice">数据库集合</param>
        /// <param name="order"></param>
        private void UpdateInvoiceLines(IEnumerable<InvoiceLineDTO> sourceInvoiceLines, BasePurchaseInvoice dstInvoice, Order order)
        {
            var invoiceLines = new List<PurchaseInvoiceLine>();
            foreach (var sourceInvoiceLine in sourceInvoiceLines)
            {
                var result = dstInvoice.InvoiceLines.FirstOrDefault(p => p.Id == sourceInvoiceLine.InvoiceLineId);
                if (result == null)
                {
                    result = InvoiceFactory.CreateInvoiceLine();
                    result.ChangeCurrentIdentity(sourceInvoiceLine.InvoiceLineId);
                }
                InvoiceFactory.SetInvoiceLine(result, sourceInvoiceLine.Amount, order,
                    sourceInvoiceLine.InvoiceLineId, sourceInvoiceLine.Note);
                invoiceLines.Add(result);
            }
            dstInvoice.InvoiceLines.ToList().ForEach(p =>
            {
                if (invoiceLines.FirstOrDefault(t => t.Id == p.Id) == null)
                {
                    _invoiceRepository.RemoveInvoiceLine(p);
                }
            });
            dstInvoice.InvoiceLines = invoiceLines;
        }

        #endregion
    }
}
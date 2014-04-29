#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 11:04:45
// 文件名：CreditNoteAppService
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
    ///     贷项单服务实现
    /// </summary>
    [LogAOP]
    public class CreditNoteAppService : ContextBoundObject, ICreditNoteAppService
    {
        private readonly ICreditNoteQuery _creditNoteQuery;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ISupplierRepository _supplierRepository;

        public CreditNoteAppService(ICreditNoteQuery creditNoteQuery,
            IInvoiceRepository invoiceRepository,
            ISupplierRepository supplierRepository,
            IOrderRepository orderRepository,
            ICurrencyRepository currencyRepository)
        {
            _creditNoteQuery = creditNoteQuery;
            _invoiceRepository = invoiceRepository;
            _supplierRepository = supplierRepository;
            _orderRepository = orderRepository;
            _currencyRepository = currencyRepository;
        }

        #region CreditNoteDTO

        /// <summary>
        ///     获取所有贷项单
        /// </summary>
        /// <returns></returns>
        public IQueryable<CreditNoteDTO> GetCreditNoteInvoices()
        {
            var queryBuilder =
                new QueryBuilder<CreditNoteInvoice>();
            return _creditNoteQuery.CreditNoteDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增贷项单。
        /// </summary>
        /// <param name="creditNoteInvoice">贷项单DTO。</param>
        [Insert(typeof(CreditNoteDTO))]
        public void InsertCreditNoteInvoice(CreditNoteDTO creditNoteInvoice)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.Id == creditNoteInvoice.SupplierId).FirstOrDefault();
            var order = _orderRepository.Get(creditNoteInvoice.OrderId);
            var currency = _currencyRepository.GetFiltered(p => p.Id == creditNoteInvoice.CurrencyId).FirstOrDefault();

            var newCreditNoteInvoice = InvoiceFactory.CreateCreditNoteInvoice(creditNoteInvoice.InvoideCode,
                creditNoteInvoice.InvoiceDate, creditNoteInvoice.OperatorName);
            var date = DateTime.Now.Date;
            var seq = _invoiceRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            newCreditNoteInvoice.SetInvoiceNumber(seq);
            newCreditNoteInvoice.SetSupplier(supplier);
            newCreditNoteInvoice.SetOrder(order);
            newCreditNoteInvoice.SetPaidAmount(creditNoteInvoice.PaidAmount);
            newCreditNoteInvoice.SetCurrency(currency);
            newCreditNoteInvoice.SetPaymentScheduleLine(creditNoteInvoice.PaymentScheduleLineId);
            newCreditNoteInvoice.SetInvoiceStatus(InvoiceStatus.草稿);
            foreach (var invoiceLine in creditNoteInvoice.InvoiceLines)
            {
                if (order != null)
                {
                    var orderLine = order.OrderLines.FirstOrDefault(p => p.Id == invoiceLine.OrderLineId);
                    newCreditNoteInvoice.AddInvoiceLine(invoiceLine.Amount, orderLine, invoiceLine.Note);
                }
                else
                {
                    newCreditNoteInvoice.AddInvoiceLine(invoiceLine.Amount, null, invoiceLine.Note);
                }
            }
            newCreditNoteInvoice.SetInvoiceValue();
            _invoiceRepository.Add(newCreditNoteInvoice);
        }

        /// <summary>
        ///     更新贷项单。
        /// </summary>
        /// <param name="creditNoteInvoice">贷项单DTO。</param>
        [Update(typeof(CreditNoteDTO))]
        public void ModifyCreditNoteInvoice(CreditNoteDTO creditNoteInvoice)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.Id == creditNoteInvoice.SupplierId).FirstOrDefault();
            var order = _orderRepository.Get(creditNoteInvoice.OrderId);
            var currency = _currencyRepository.GetFiltered(p => p.Id == creditNoteInvoice.CurrencyId).FirstOrDefault();

            var updateCreditNoteInvoice = _invoiceRepository.GetBasePurchaseInvoice(creditNoteInvoice.CreditNoteId);
            //获取需要更新的对象。
            if (updateCreditNoteInvoice != null)
            {
                InvoiceFactory.SetInvoice(updateCreditNoteInvoice, creditNoteInvoice.InvoideCode,
                    creditNoteInvoice.InvoiceDate, creditNoteInvoice.OperatorName, creditNoteInvoice.InvoiceNumber,
                    supplier, order,
                    creditNoteInvoice.PaidAmount, currency, creditNoteInvoice.PaymentScheduleLineId,
                    creditNoteInvoice.Status);
                //更新主表。

                UpdateInvoiceLines(creditNoteInvoice.InvoiceLines, updateCreditNoteInvoice, order);
                //更新从表。
            }
            _invoiceRepository.Modify(updateCreditNoteInvoice);
        }

        /// <summary>
        ///     删除贷项单。
        /// </summary>
        /// <param name="creditNoteInvoice">贷项单DTO。</param>
        [Delete(typeof(CreditNoteDTO))]
        public void DeleteCreditNoteInvoice(CreditNoteDTO creditNoteInvoice)
        {
            if (creditNoteInvoice == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delCreditNoteInvoice = _invoiceRepository.GetBasePurchaseInvoice(creditNoteInvoice.CreditNoteId);
            //获取需要删除的对象。
            if (delCreditNoteInvoice != null)
            {
                _invoiceRepository.DeleteInvoice(delCreditNoteInvoice); //删除贷项单。
            }
        }

        #endregion

        #region 更新贷项单行集合

        /// <summary>
        ///     更新贷项单行集合
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
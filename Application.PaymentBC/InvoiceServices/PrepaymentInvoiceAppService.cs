#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 11:04:17
// 文件名：PrepaymentInvoiceAppService
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
    ///     预付款发票服务实现
    /// </summary>
   [LogAOP]
    public class PrepaymentInvoiceAppService : ContextBoundObject, IPrepaymentInvoiceAppService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPrepaymentInvoiceQuery _prepaymentInvoiceQuery;
        private readonly ISupplierRepository _supplierRepository;

        public PrepaymentInvoiceAppService(IPrepaymentInvoiceQuery prepaymentInvoiceQuery,
            IInvoiceRepository invoiceRepository,
            ISupplierRepository supplierRepository,
            IOrderRepository orderRepository,
            ICurrencyRepository currencyRepository)
        {
            _prepaymentInvoiceQuery = prepaymentInvoiceQuery;
            _invoiceRepository = invoiceRepository;
            _supplierRepository = supplierRepository;
            _orderRepository = orderRepository;
            _currencyRepository = currencyRepository;
        }

        #region PrepaymentInvoiceDTO

        /// <summary>
        ///     获取所有预付款发票
        /// </summary>
        /// <returns></returns>
        public IQueryable<PrepaymentInvoiceDTO> GetPrepaymentInvoices()
        {
            var queryBuilder =
                new QueryBuilder<PrepaymentInvoice>();
            return _prepaymentInvoiceQuery.PrepaymentInvoiceDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增预付款发票。
        /// </summary>
        /// <param name="prepaymentInvoice">预付款发票DTO。</param>
        [Insert(typeof (PrepaymentInvoiceDTO))]
        public void InsertPrepaymentInvoice(PrepaymentInvoiceDTO prepaymentInvoice)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.Id == prepaymentInvoice.SupplierId).FirstOrDefault();
            var order = _orderRepository.Get(prepaymentInvoice.OrderId);
            var currency = _currencyRepository.GetFiltered(p => p.Id == prepaymentInvoice.CurrencyId).FirstOrDefault();

            var newPrepaymentInvoice = InvoiceFactory.CreatePrepaymentInvoice(prepaymentInvoice.InvoideCode,
                prepaymentInvoice.InvoiceDate, prepaymentInvoice.OperatorName);
            var date = DateTime.Now.Date;
            var seq = _invoiceRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            newPrepaymentInvoice.SetInvoiceNumber(seq);
            newPrepaymentInvoice.SetSupplier(supplier);
            newPrepaymentInvoice.SetOrder(order);
            newPrepaymentInvoice.SetPaidAmount(prepaymentInvoice.PaidAmount);
            newPrepaymentInvoice.SetCurrency(currency);
            newPrepaymentInvoice.SetPaymentScheduleLine(prepaymentInvoice.PaymentScheduleLineId);
            newPrepaymentInvoice.SetInvoiceStatus(InvoiceStatus.草稿);
            foreach (var invoiceLine in prepaymentInvoice.InvoiceLines)
            {
                if (order != null)
                {
                    var orderLine = order.OrderLines.FirstOrDefault(p => p.Id == invoiceLine.OrderLineId);
                    newPrepaymentInvoice.AddInvoiceLine(invoiceLine.ItemName, invoiceLine.Amount, orderLine,
                        invoiceLine.Note);
                }
                else
                {
                    newPrepaymentInvoice.AddInvoiceLine(invoiceLine.ItemName, invoiceLine.Amount, null, invoiceLine.Note);
                }
            }
            newPrepaymentInvoice.SetInvoiceValue();
            _invoiceRepository.Add(newPrepaymentInvoice);
        }

        /// <summary>
        ///     更新预付款发票。
        /// </summary>
        /// <param name="prepaymentInvoice">预付款发票DTO。</param>
        [Update(typeof (PrepaymentInvoiceDTO))]
        public void ModifyPrepaymentInvoice(PrepaymentInvoiceDTO prepaymentInvoice)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.Id == prepaymentInvoice.SupplierId).FirstOrDefault();
            var order = _orderRepository.Get(prepaymentInvoice.OrderId);
            var currency = _currencyRepository.GetFiltered(p => p.Id == prepaymentInvoice.CurrencyId).FirstOrDefault();

            var updatePrepaymentInvoice = _invoiceRepository.Get(prepaymentInvoice.PrepaymentInvoiceId);
            //获取需要更新的对象。
            if (updatePrepaymentInvoice != null)
            {
                InvoiceFactory.SetInvoice(updatePrepaymentInvoice, prepaymentInvoice.InvoideCode,
                    prepaymentInvoice.InvoiceDate, prepaymentInvoice.OperatorName, prepaymentInvoice.InvoiceNumber,
                    supplier, order,
                    prepaymentInvoice.PaidAmount, currency, prepaymentInvoice.PaymentScheduleLineId,
                    prepaymentInvoice.Status);
                //更新主表。

                UpdateInvoiceLines(prepaymentInvoice.InvoiceLines, updatePrepaymentInvoice, order);
                //更新从表。
            }
            _invoiceRepository.Modify(updatePrepaymentInvoice);
        }

        /// <summary>
        ///     删除预付款发票。
        /// </summary>
        /// <param name="prepaymentInvoice">预付款发票DTO。</param>
        [Delete(typeof (PrepaymentInvoiceDTO))]
        public void DeletePrepaymentInvoice(PrepaymentInvoiceDTO prepaymentInvoice)
        {
            if (prepaymentInvoice == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delPrepaymentInvoice = _invoiceRepository.Get(prepaymentInvoice.PrepaymentInvoiceId);
            //获取需要删除的对象。
            if (delPrepaymentInvoice != null)
            {
                _invoiceRepository.DeleteInvoice(delPrepaymentInvoice); //删除预付款发票。
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
        private void UpdateInvoiceLines(IEnumerable<InvoiceLineDTO> sourceInvoiceLines, Invoice dstInvoice, Order order)
        {
            var invoiceLines = new List<InvoiceLine>();
            foreach (var sourceInvoiceLine in sourceInvoiceLines)
            {
                var result = dstInvoice.InvoiceLines.FirstOrDefault(p => p.Id == sourceInvoiceLine.InvoiceLineId);
                if (result == null)
                {
                    result = InvoiceFactory.CreateInvoiceLine();
                    result.ChangeCurrentIdentity(sourceInvoiceLine.InvoiceLineId);
                }
                InvoiceFactory.SetInvoiceLine(result, sourceInvoiceLine.ItemName, sourceInvoiceLine.Amount, order,
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
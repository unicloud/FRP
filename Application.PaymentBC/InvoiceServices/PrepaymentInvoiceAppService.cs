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
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.InvoiceQueries;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PaymentBC.Enums;

#endregion

namespace UniCloud.Application.PaymentBC.InvoiceServices
{
    /// <summary>
    ///     预付款发票服务实现
    /// </summary>
    public class PrepaymentInvoiceAppService : IPrepaymentInvoiceAppService
    {
        private readonly IPrepaymentInvoiceQuery _prepaymentInvoiceQuery;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrencyRepository _currencyRepository;
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
        [Insert(typeof(PrepaymentInvoiceDTO))]
        public void InsertPrepaymentInvoice(PrepaymentInvoiceDTO prepaymentInvoice)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.Id == prepaymentInvoice.SupplierId).FirstOrDefault();
            var order = _orderRepository.GetFiltered(p => p.Id == prepaymentInvoice.OrderId).FirstOrDefault();
            var currency = _currencyRepository.GetFiltered(p => p.Id == prepaymentInvoice.CurrencyId).FirstOrDefault();

            var newPrepaymentInvoice = InvoiceFactory.CreatePrepaymentInvoice(prepaymentInvoice.InvoideCode, prepaymentInvoice.InvoiceDate, prepaymentInvoice.OperatorName);
            newPrepaymentInvoice.SetInvoiceNumber(1);
            newPrepaymentInvoice.SetSupplier(supplier);
            newPrepaymentInvoice.SetInvoiceValue();
            newPrepaymentInvoice.SetOrder(order);
            newPrepaymentInvoice.SetPaidAmount(prepaymentInvoice.PaidAmount);
            newPrepaymentInvoice.Review(prepaymentInvoice.Reviewer);
            newPrepaymentInvoice.SetCurrency(currency);
            newPrepaymentInvoice.SetPaymentScheduleLine(prepaymentInvoice.PaymentScheduleLineId);
            newPrepaymentInvoice.SetInvoiceStatus(InvoiceStatus.草稿);
            foreach (var invoiceLine in prepaymentInvoice.InvoiceLines)
            {
                if (order != null)
                {
                    var orderLine = order.OrderLines.FirstOrDefault(p => p.Id == invoiceLine.OrderLineId);
                    newPrepaymentInvoice.AddInvoiceLine(invoiceLine.ItemName, invoiceLine.Amount, orderLine);
                }
                else
                {
                    newPrepaymentInvoice.AddInvoiceLine(invoiceLine.ItemName, invoiceLine.Amount, null);
                }
            }
        }

        /// <summary>
        ///     更新预付款发票。
        /// </summary>
        /// <param name="prepaymentInvoice">预付款发票DTO。</param>
        [Update(typeof(PrepaymentInvoiceDTO))]
        public void ModifyPrepaymentInvoice(PrepaymentInvoiceDTO prepaymentInvoice)
        {
            var updatePrepaymentInvoice = _invoiceRepository.GetFiltered(t => t.Id == prepaymentInvoice.PrepaymentInvoiceId).FirstOrDefault();
            //获取需要更新的对象。
            if (updatePrepaymentInvoice != null)
            {
                //更新主表。


                //更新从表。
            }
            _invoiceRepository.Modify(updatePrepaymentInvoice);
        }

        /// <summary>
        ///     删除预付款发票。
        /// </summary>
        /// <param name="prepaymentInvoice">预付款发票DTO。</param>
        [Delete(typeof(PrepaymentInvoiceDTO))]
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
                _invoiceRepository.DeleteInvoice(delPrepaymentInvoice);//删除预付款发票。
            }
        }

        #endregion
    }
}

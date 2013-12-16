#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 11:03:01
// 文件名：LeaseInvoiceAppService
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
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Application.PaymentBC.InvoiceServices
{
    /// <summary>
    ///     租赁发票服务实现
    /// </summary>
    public class LeaseInvoiceAppService : ILeaseInvoiceAppService
    {
        private readonly ILeaseInvoiceQuery _leaseInvoiceQuery;
        private readonly IInvoiceRepository _invoiceRepository;

        public LeaseInvoiceAppService(ILeaseInvoiceQuery leaseInvoiceQuery,
            IInvoiceRepository invoiceRepository)
        {
            _leaseInvoiceQuery = leaseInvoiceQuery;
            _invoiceRepository = invoiceRepository;
        }

        #region LeaseInvoiceDTO

        /// <summary>
        ///     获取所有租赁发票
        /// </summary>
        /// <returns></returns>
        public IQueryable<LeaseInvoiceDTO> GetLeaseInvoices()
        {
            var queryBuilder =
                new QueryBuilder<LeaseInvoice>();
            return _leaseInvoiceQuery.LeaseInvoiceDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增租赁发票。
        /// </summary>
        /// <param name="leaseInvoice">租赁发票DTO。</param>
        [Insert(typeof(LeaseInvoiceDTO))]
        public void InsertLeaseInvoice(LeaseInvoiceDTO leaseInvoice)
        {
            var newLeaseInvoice = InvoiceFactory.CreateLeaseInvoice();
            newLeaseInvoice.SetInvoiceNumber(1);

        }

        /// <summary>
        ///     更新租赁发票。
        /// </summary>
        /// <param name="leaseInvoice">租赁发票DTO。</param>
        [Update(typeof(LeaseInvoiceDTO))]
        public void ModifyLeaseInvoice(LeaseInvoiceDTO leaseInvoice)
        {
            var updateLeaseInvoice = _invoiceRepository.GetFiltered(t => t.Id == leaseInvoice.LeaseInvoiceId).FirstOrDefault();
            //获取需要更新的对象。
            if (updateLeaseInvoice != null)
            {
                //更新主表。


                //更新从表。
            }
            _invoiceRepository.Modify(updateLeaseInvoice);
        }

        /// <summary>
        ///     删除租赁发票。
        /// </summary>
        /// <param name="leaseInvoice">租赁发票DTO。</param>
        [Delete(typeof(LeaseInvoiceDTO))]
        public void DeleteLeaseInvoice(LeaseInvoiceDTO leaseInvoice)
        {
            if (leaseInvoice == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delLeaseInvoice = _invoiceRepository.Get(leaseInvoice.LeaseInvoiceId);
            //获取需要删除的对象。
            if (delLeaseInvoice != null)
            {
                _invoiceRepository.DeleteInvoice(delLeaseInvoice);//删除租赁发票。
            }
        }

        #endregion
    }
}

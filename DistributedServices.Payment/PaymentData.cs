//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.InvoiceServices;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.Payment
{
    /// <summary>
    /// 应付款模块数据类
    /// </summary>
    public class PaymentData : ExposeData.ExposeData
    {
        //private readonly IPaymentAppService _flightLogAppService = Container.Current.Resolve<IPaymentAppService>();

        private readonly ICreditNoteAppService _creditNoteAppService;
        private readonly ILeaseInvoiceAppService _leaseInvoiceAppService;
        private readonly IPrepaymentInvoiceAppService _prepaymentInvoiceAppService;
        private readonly IPurchaseInvoiceAppService _purchaseInvoiceAppService;

        public PaymentData()
            : base("UniCloud.Application.PaymentBC.DTO")
        {
            _creditNoteAppService = DefaultContainer.Resolve<ICreditNoteAppService>();
            _leaseInvoiceAppService = DefaultContainer.Resolve<ILeaseInvoiceAppService>();
            _prepaymentInvoiceAppService = DefaultContainer.Resolve<IPrepaymentInvoiceAppService>();
            _purchaseInvoiceAppService = DefaultContainer.Resolve<IPurchaseInvoiceAppService>();

        }

        public IQueryable<MaintainInvoiceDTO> MaintainInvoices
        {
            get { return null; }
        }

        #region Invoice集合

        /// <summary>
        ///     贷项单集合
        /// </summary>
        //public IQueryable<CreditNoteDTO> CreditNotes
        //{
        //    get { return  null; }
        //}

        /// <summary>
        ///     租赁发票集合
        /// </summary>
        public IQueryable<LeaseInvoiceDTO> LeaseInvoices
        {
            get { return _leaseInvoiceAppService.GetLeaseInvoices(); }
        }

        /// <summary>
        ///     预付款集合
        /// </summary>
        public IQueryable<PrepaymentInvoiceDTO> PrepaymentInvoices
        {
            get { return _prepaymentInvoiceAppService.GetPrepaymentInvoices(); }
        }

        /// <summary>
        ///     采购发票集合
        /// </summary>
        public IQueryable<PurchaseInvoiceDTO> PurchaseInvoices
        {
            get { return _purchaseInvoiceAppService.GetPurchaseInvoices(); }
        }

        #endregion
    }
}
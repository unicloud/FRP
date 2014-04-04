#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.ContractAircraftServices;
using UniCloud.Application.PaymentBC.ContractEngineServices;
using UniCloud.Application.PaymentBC.CurrencyServices;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.GuaranteeServices;
using UniCloud.Application.PaymentBC.InvoiceServices;
using UniCloud.Application.PaymentBC.MaintainContractServices;
using UniCloud.Application.PaymentBC.MaintainInvoiceServices;
using UniCloud.Application.PaymentBC.OrderServices;
using UniCloud.Application.PaymentBC.PaymentNoticeServices;
using UniCloud.Application.PaymentBC.PaymentScheduleServices;
using UniCloud.Application.PaymentBC.SupplierServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Payment
{
    /// <summary>
    ///     应付款模块数据类
    /// </summary>
    public class PaymentData : ExposeData.ExposeData
    {
        private readonly IContractAircraftAppService _contractAircraftAppService;
        private readonly IContractEngineAppService _contractEngineAppService;
        private readonly ICreditNoteAppService _creditNoteAppService;
        private readonly ICurrencyAppService _currencyAppService;
        private readonly IGuaranteeAppService _guaranteeAppService;
        private readonly ILeaseInvoiceAppService _leaseInvoiceAppService;
        private readonly IMaintainContractAppService _maintainContractAppService;
        private readonly IMaintainInvoiceAppService _maintainInvoiceAppService;
        private readonly IOrderAppService _orderAppService;
        private readonly IPaymentNoticeAppService _paymentNoticeAppService;
        private readonly IPaymentScheduleAppService _paymentScheduleAppService;
        private readonly IPrepaymentInvoiceAppService _prepaymentInvoiceAppService;
        private readonly IPurchaseInvoiceAppService _purchaseInvoiceAppService;
        private readonly ISupplierAppService _supplierAppService;

        public PaymentData()
            : base("UniCloud.Application.PaymentBC.DTO")
        {
            _creditNoteAppService = DefaultContainer.Resolve<ICreditNoteAppService>();
            _currencyAppService = DefaultContainer.Resolve<ICurrencyAppService>();
            _leaseInvoiceAppService = DefaultContainer.Resolve<ILeaseInvoiceAppService>();
            _prepaymentInvoiceAppService = DefaultContainer.Resolve<IPrepaymentInvoiceAppService>();
            _purchaseInvoiceAppService = DefaultContainer.Resolve<IPurchaseInvoiceAppService>();
            _maintainInvoiceAppService = DefaultContainer.Resolve<IMaintainInvoiceAppService>();
            _contractAircraftAppService = DefaultContainer.Resolve<IContractAircraftAppService>();
            _contractEngineAppService = DefaultContainer.Resolve<IContractEngineAppService>();
            _orderAppService = DefaultContainer.Resolve<IOrderAppService>();
            _paymentScheduleAppService = DefaultContainer.Resolve<IPaymentScheduleAppService>();
            _paymentNoticeAppService = DefaultContainer.Resolve<IPaymentNoticeAppService>();
            _guaranteeAppService = DefaultContainer.Resolve<IGuaranteeAppService>();
            _maintainContractAppService = DefaultContainer.Resolve<IMaintainContractAppService>();
            _supplierAppService = DefaultContainer.Resolve<ISupplierAppService>();
        }

        #region Invoice集合

        /// <summary>
        ///     贷项单集合
        /// </summary>
        public IQueryable<CreditNoteDTO> CreditNotes
        {
            get { return _creditNoteAppService.GetCreditNoteInvoices(); }
        }

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

        #region 维修发票

        /// <summary>
        ///     发动机维修发票集合
        /// </summary>
        public IQueryable<BaseMaintainInvoiceDTO> MaintainInvoices
        {
            get { return _maintainInvoiceAppService.GetMaintainInvoices(); }
        }

        /// <summary>
        ///     发动机维修发票集合
        /// </summary>
        public IQueryable<EngineMaintainInvoiceDTO> EngineMaintainInvoices
        {
            get { return _maintainInvoiceAppService.GetEngineMaintainInvoices(); }
        }

        /// <summary>
        ///     APU维修发票集合
        /// </summary>
        public IQueryable<APUMaintainInvoiceDTO> APUMaintainInvoices
        {
            get { return _maintainInvoiceAppService.GetApuMaintainInvoices(); }
        }

        /// <summary>
        ///     机身维修发票集合
        /// </summary>
        public IQueryable<AirframeMaintainInvoiceDTO> AirframeMaintainInvoices
        {
            get { return _maintainInvoiceAppService.GetAirframeMaintainInvoices(); }
        }

        /// <summary>
        ///     起落架维修发票集合
        /// </summary>
        public IQueryable<UndercartMaintainInvoiceDTO> UndercartMaintainInvoices
        {
            get { return _maintainInvoiceAppService.GetUndercartMaintainInvoices(); }
        }

        #endregion

        #region 合同飞机

        /// <summary>
        ///     合同飞机集合
        /// </summary>
        public IQueryable<ContractAircraftDTO> ContractAircrafts
        {
            get { return _contractAircraftAppService.GetContractAircrafts(); }
        }

        #endregion

        #region 合同发动机

        /// <summary>
        ///     合同发动机集合
        /// </summary>
        public IQueryable<ContractEngineDTO> ContractEngines
        {
            get { return _contractEngineAppService.GetContractEngines(); }
        }

        #endregion

        #region 币种

        /// <summary>
        ///     币种集合
        /// </summary>
        public IQueryable<CurrencyDTO> Currencies
        {
            get { return GetStaticData("currenciesPayment", () => _currencyAppService.GetCurrencies()); }
        }

        #endregion

        #region 付款计划集合

        /// <summary>
        ///     所有付款计划
        /// </summary>
        public IQueryable<PaymentScheduleDTO> PaymentSchedules
        {
            get { return _paymentScheduleAppService.GetPaymentSchedules(); }
        }

        /// <summary>
        ///     飞机付款计划
        /// </summary>
        public IQueryable<AcPaymentScheduleDTO> AcPaymentSchedules
        {
            get { return _paymentScheduleAppService.GetAcPaymentSchedules(); }
        }


        /// <summary>
        ///     发动机付款计划
        /// </summary>
        public IQueryable<EnginePaymentScheduleDTO> EnginePaymentSchedules
        {
            get { return _paymentScheduleAppService.GetEnginePaymentSchedules(); }
        }

        /// <summary>
        ///     标准付款计划
        /// </summary>
        public IQueryable<StandardPaymentScheduleDTO> StandardPaymentSchedules
        {
            get { return _paymentScheduleAppService.GetStandardPaymentSchedules(); }
        }

        #endregion

        #region 付款通知

        /// <summary>
        ///     付款通知集合
        /// </summary>
        public IQueryable<PaymentNoticeDTO> PaymentNotices
        {
            get { return _paymentNoticeAppService.GetPaymentNotices(); }
        }

        #endregion

        #region 供应商

        /// <summary>
        ///     供应商集合
        /// </summary>
        public IQueryable<SupplierDTO> Suppliers
        {
            get { return GetStaticData("suppliersPayment", () => _supplierAppService.GetSuppliers()); }
        }

        #endregion

        #region 订单

        /// <summary>
        ///     所有订单集合
        /// </summary>
        public IQueryable<OrderDTO> Orders
        {
            get { return _orderAppService.GetOrders(); }
        }

        /// <summary>
        ///     所有采购订单集合
        /// </summary>
        public IQueryable<PurchaseOrderDTO> PurchaseOrders
        {
            get { return _orderAppService.GetPurchaseOrders(); }
        }

        /// <summary>
        ///     所有租赁订单集合（不包含订单行）
        /// </summary>
        public IQueryable<LeaseOrderDTO> LeaseOrders
        {
            get { return _orderAppService.GetLeaseOrders(); }
        }

        /// <summary>
        ///     飞机采购订单集合
        /// </summary>
        public IQueryable<AircraftPurchaseOrderDTO> AircraftPurchaseOrders
        {
            get { return _orderAppService.GetAircraftPurchaseOrders(); }
        }

        /// <summary>
        ///     飞机租赁订单集合
        /// </summary>
        public IQueryable<AircraftLeaseOrderDTO> AircraftLeaseOrders
        {
            get { return _orderAppService.GetAircraftLeaseOrders(); }
        }

        /// <summary>
        ///     发动机采购订单集合
        /// </summary>
        public IQueryable<EnginePurchaseOrderDTO> EnginePurchaseOrders
        {
            get { return _orderAppService.GetEnginePurchaseOrders(); }
        }

        /// <summary>
        ///     发动机租赁订单集合
        /// </summary>
        public IQueryable<EngineLeaseOrderDTO> EngineLeaseOrders
        {
            get { return _orderAppService.GetEngineLeaseOrders(); }
        }

        /// <summary>
        ///     BFE订单集合
        /// </summary>
        public IQueryable<BFEPurchaseOrderDTO> BFEPurchaseOrders
        {
            get { return _orderAppService.GetBFEPurchaseOrders(); }
        }

        /// <summary>
        ///     标准订单集合
        /// </summary>
        public IQueryable<StandardOrderDTO> StandardOrders
        {
            get { return _orderAppService.GetStandardOrders(); }
        }

        #endregion

        #region 保证金集合

        /// <summary>
        ///     租赁保证金
        /// </summary>
        public IQueryable<LeaseGuaranteeDTO> LeaseGuarantees
        {
            get { return _guaranteeAppService.GetLeaseGuarantees(); }
        }

        /// <summary>
        ///     大修保证金
        /// </summary>
        public IQueryable<MaintainGuaranteeDTO> MaintainGuarantees
        {
            get { return _guaranteeAppService.GetMaintainGuarantee(); }
        }

        #endregion

        #region 维修合同

        /// <summary>
        ///     维修合同
        /// </summary>
        public IQueryable<MaintainContractDTO> MaintainContracts
        {
            get { return _maintainContractAppService.GetMaintainContracts(); }
        }

        #endregion
    }
}
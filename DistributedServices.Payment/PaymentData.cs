
﻿#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC;
using UniCloud.Application.PaymentBC.ContractAircraftServices;
using UniCloud.Application.PaymentBC.ContractEngineServices;
using UniCloud.Application.PaymentBC.CurrencyServices;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.InvoiceServices;
using UniCloud.Application.PaymentBC.MaintainInvoiceServices;
using UniCloud.Application.PaymentBC.OrderServices;
using UniCloud.Application.PaymentBC.PaymentScheduleServices;using UniCloud.Application.PaymentBC.PaymentScheduleServices;
using UniCloud.Application.PaymentBC.SupplierServices;using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Payment
{
    /// <summary>
    ///     应付款模块数据类
    /// </summary>
    public class PaymentData : ExposeData.ExposeData
    {
        //private readonly IPaymentAppService _flightLogAppService = Container.Current.Resolve<IPaymentAppService>();

        private readonly ICreditNoteAppService _creditNoteAppService;
        private readonly ILeaseInvoiceAppService _leaseInvoiceAppService;
        private readonly IMaintainInvoiceAppService _maintainInvoiceAppService;
        private readonly IPrepaymentInvoiceAppService _prepaymentInvoiceAppService;
        private readonly IPurchaseInvoiceAppService _purchaseInvoiceAppService;
        private readonly IContractAircraftAppService _contractAircraftAppService;
        private readonly IContractEngineAppService _contractEngineAppService;
        private readonly ICurrencyAppService _currencyAppService;
        private readonly IOrderAppService _orderAppService;
        private readonly IPaymentScheduleAppService _paymentScheduleAppService;
        private readonly ISupplierAppService _supplierAppService;
        private readonly IStaticLoad _staticLoad;
        public PaymentData()
            : base("UniCloud.Application.PaymentBC.DTO")
        {
            _staticLoad = DefaultContainer.Resolve<IStaticLoad>();
            _creditNoteAppService = DefaultContainer.Resolve<ICreditNoteAppService>();
            _leaseInvoiceAppService = DefaultContainer.Resolve<ILeaseInvoiceAppService>();
            _prepaymentInvoiceAppService = DefaultContainer.Resolve<IPrepaymentInvoiceAppService>();
            _purchaseInvoiceAppService = DefaultContainer.Resolve<IPurchaseInvoiceAppService>();
            _maintainInvoiceAppService = DefaultContainer.Resolve<IMaintainInvoiceAppService>();
            _contractAircraftAppService = DefaultContainer.Resolve<IContractAircraftAppService>();
            _contractEngineAppService = DefaultContainer.Resolve<IContractEngineAppService>();
            _currencyAppService = DefaultContainer.Resolve<ICurrencyAppService>();
            _orderAppService = DefaultContainer.Resolve<IOrderAppService>();            
            _paymentScheduleAppService = DefaultContainer.Resolve<IPaymentScheduleAppService>();
            _supplierAppService = DefaultContainer.Resolve<ISupplierAppService>();        
        }

        #region Invoice集合

        //public IQueryable<CreditNoteDTO> CreditNotes
        //{
        //    get { return  null; }
        //}
        /// <summary>
        ///     贷项单集合
        /// </summary>
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
            get { return _staticLoad.GetCurrencies(); }
        }

        #endregion

        #region 付款计划集合


        /// <summary>
        /// 飞机付款计划
        /// </summary>
        public IQueryable<AcPaymentScheduleDTO> AcPaymentSchedules
        {
            get { return _paymentScheduleAppService.GetAcPaymentSchedules(); }
        }


        /// <summary>
        /// 发动机付款计划
        /// </summary>
        public IQueryable<EnginePaymentScheduleDTO> EnginePaymentSchedules
        {
            get { return _paymentScheduleAppService.GetEnginePaymentSchedules(); }
        }

        #endregion

        #region 供应商
        /// <summary>
        ///  供应商集合
        /// </summary>
        public IQueryable<SupplierDTO> Suppliers
        {
            get { return _staticLoad.GetSuppliers(); }
        }
        #endregion

        #region 订单


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
        #endregion    
    }
}
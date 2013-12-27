//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using UniCloud.Application.PaymentBC;
using UniCloud.Application.PaymentBC.ContractAircraftServices;
using UniCloud.Application.PaymentBC.ContractEngineServices;
using UniCloud.Application.PaymentBC.CurrencyServices;
using UniCloud.Application.PaymentBC.GuaranteeServices;
using UniCloud.Application.PaymentBC.InvoiceServices;
using UniCloud.Application.PaymentBC.MaintainContractServices;
using UniCloud.Application.PaymentBC.MaintainInvoiceServices;
using UniCloud.Application.PaymentBC.OrderServices;
using UniCloud.Application.PaymentBC.PaymentNoticeServices;
using UniCloud.Application.PaymentBC.PaymentScheduleServices;
using UniCloud.Application.PaymentBC.Query.ContractAircraftQueries;
using UniCloud.Application.PaymentBC.Query.ContractEngineQueries;
using UniCloud.Application.PaymentBC.Query.CurrencyQueries;
using UniCloud.Application.PaymentBC.Query.GuaranteeQueries;
using UniCloud.Application.PaymentBC.Query.InvoiceQueries;
using UniCloud.Application.PaymentBC.Query.MaintainContractQueries;
using UniCloud.Application.PaymentBC.Query.MaintainInvoiceQueries;
using UniCloud.Application.PaymentBC.Query.OrderQueries;
using UniCloud.Application.PaymentBC.Query.PaymentNoticeQueries;
using UniCloud.Application.PaymentBC.Query.PaymentScheduleQueries;
using UniCloud.Application.PaymentBC.Query.SupplierQueries;
using UniCloud.Application.PaymentBC.SupplierServices;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.GuaranteeAgg;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentNoticeAgg;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentScheduleAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PaymentBC.Repositories;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Payment.InstanceProviders
{
    /// <summary>
    ///     DI 容器
    /// </summary>
    public static class Container
    {
        #region 方法

        public static void ConfigureContainer()
        {
            Configuration.Create()
                .UseAutofac()
                //.UserCaching()
                .CreateLog()
                .Register<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IStaticLoad, StaticLoad>()

                #region 发票相关配置，包括查询，应用服务，仓储注册

                .Register<ICreditNoteQuery, CreditNoteQuery>()
                .Register<ILeaseInvoiceQuery, LeaseInvoiceQuery>()
                .Register<IPrepaymentInvoiceQuery, PrepaymentInvoiceQuery>()
                .Register<IPurchaseInvoiceQuery, PurchaseInvoiceQuery>()
                .Register<ICreditNoteAppService, CreditNoteAppService>()
                .Register<ILeaseInvoiceAppService, LeaseInvoiceAppService>()
                .Register<IPrepaymentInvoiceAppService, PrepaymentInvoiceAppService>()
                .Register<IPurchaseInvoiceAppService, PurchaseInvoiceAppService>()
                .Register<IInvoiceRepository, InvoiceRepository>()
                #endregion

                #region 维修发票相关配置，包括查询，应用服务，仓储注册

                .Register<IMaintainInvoiceQuery, MaintainInvoiceQuery>()
                .Register<IMaintainInvoiceAppService, MaintainInvoiceAppService>()
                .Register<IMaintainInvoiceRepository, MaintainInvoiceRepository>()
                #endregion

                #region 合同飞机相关配置，包括查询，应用服务，仓储注册

                .Register<IContractAircraftQuery, ContractAircraftQuery>()
                .Register<IContractAircraftAppService, ContractAircraftAppService>()
                #endregion

                #region 合同发动机相关配置，包括查询，应用服务，仓储注册

                .Register<IContractEngineQuery, ContractEngineQuery>()
                .Register<IContractEngineAppService, ContractEngineAppService>()
                #endregion

                #region   币种相关配置，包括查询，应用服务，仓储注册

                .Register<ICurrencyQuery, CurrencyQuery>()
                .Register<ICurrencyAppService, CurrencyAppService>()
                .Register<ICurrencyRepository, CurrencyRepository>()
                #endregion

                #region 供应商相关配置，包括查询，应用服务，仓储注册

                .Register<ISupplierRepository, SupplierRepository>()
                .Register<ISupplierAppService, SupplierAppService>()
                .Register<ISupplierQuery, SupplierQuery>()
                #endregion

                #region 交易相关配置，包括查询，应用服务，仓储注册

                .Register<IOrderQuery, OrderQuery>()
                .Register<IOrderAppService, OrderAppService>()
                .Register<IOrderRepository, OrderRepository>()
                #endregion

                #region   付款计划相关配置，包括查询，应用服务，仓储注册

                .Register<IPaymentScheduleQuery, PaymentScheduleQuery>()
                .Register<IPaymentScheduleAppService, PaymentScheduleAppService>()
                //(null, new Interceptor<InterfaceInterceptor>(), 
                //       new InterceptionBehavior<CachingBehavior>())
                .Register<IPaymentScheduleRepository, PaymentScheduleRepository>()

                #endregion

                #region   付款通知相关配置，包括查询，应用服务，仓储注册

                .Register<IPaymentNoticeQuery, PaymentNoticeQuery>()
                .Register<IPaymentNoticeAppService, PaymentNoticeAppService>()
                .Register<IPaymentNoticeRepository, PaymentNoticeRepository>()

                #endregion

                #region  保函相关配置，包括查询，应用服务，仓储注册

                .Register<IGuaranteeQuery, GuaranteeQuery>()
                .Register<IGuaranteeAppService, GuaranteeAppService>()
                .Register<IGuaranteeRepository, GuaranteeRepository>()

                #endregion

                #region  维修合同相关配置，包括查询，应用服务，仓储注册

                .Register<IMaintainContractQuery, MaintainContractQuery>()
                .Register<IMaintainContractAppService, MaintainContractAppService>()

                #endregion

                ;
        }

        #endregion
    }
}

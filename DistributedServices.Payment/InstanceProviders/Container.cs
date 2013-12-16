//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using UniCloud.Application.PaymentBC.InvoiceServices;
using UniCloud.Application.PaymentBC.Query.InvoiceQueries;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PaymentBC.Repositories;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.Payment.InstanceProviders
{

    /// <summary>
    /// DI 容器
    /// </summary>
    public static class Container
    {
        #region 方法

        public static void ConfigureContainer()
        {

            Configuration.Create()
                .UseAutofac()
                .CreateLog()
                .Register<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager())

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

                ;
        }

        #endregion
    }
}
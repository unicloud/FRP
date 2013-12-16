//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using UniCloud.Application.PaymentBC.ContractAircraftServices;
using UniCloud.Application.PaymentBC.ContractEngineServices;
using UniCloud.Application.PaymentBC.CurrencyServices;
using UniCloud.Application.PaymentBC.Query.ContractAircraftQueries;
using UniCloud.Application.PaymentBC.Query.ContractEngineQueries;
using UniCloud.Application.PaymentBC.Query.CurrencyQueries;
using UniCloud.Infrastructure.Data;
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
                         .CreateLog()
                         .Register<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager())
             
                #region 合同飞机相关配置，包括查询，应用服务

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
                #endregion

                ;
        }

        #endregion
    }
}
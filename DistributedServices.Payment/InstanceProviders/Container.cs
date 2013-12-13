//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.Payment.InstanceProviders
{
    using Application.PaymentBC.Services;
    using Infrastructure.Crosscutting.Logging;
    using Infrastructure.Crosscutting.NetFramework.Logging;
    using Microsoft.Practices.Unity;

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
                .Register<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager());

          

        }

        #endregion
    }
}
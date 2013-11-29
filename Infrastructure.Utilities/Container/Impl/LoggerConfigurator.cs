using Microsoft.Practices.Unity;
using UniCloud.Infrastructure.Utilities.Container.Interface;

namespace UniCloud.Infrastructure.Utilities.Container.Impl
{
    public  class LoggerConfigurator:ConfiguratorBase,ILoggerConfigurator
    {
        public LoggerConfigurator(IConfigurator context) : base(context)
        {
        }

        public override IUnityContainer Configure()
        {
            LoggerFactory.SetCurrent(new UniCloudLogFactory());
            return Context.Configure();
        }
    }
}
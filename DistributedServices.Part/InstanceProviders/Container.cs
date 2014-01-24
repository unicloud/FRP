//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

namespace UniCloud.DistributedServices.Part.InstanceProviders
{
    using Application.PartBC.Services;
    using Infrastructure.Crosscutting.Logging;
    using Infrastructure.Crosscutting.NetFramework.Logging;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// DI 容器
    /// </summary>
    public static class Container
    {
        #region 属性

        /// <summary>
        /// 当前 DI 容器
        /// </summary>
        public static IUnityContainer Current { get; private set; }

        #endregion

        #region 构造函数

        static Container()
        {
            ConfigureContainer();
            ConfigureFactories();
        }

        #endregion

        #region 方法

        static void ConfigureContainer()
        {

            Current = new UnityContainer();


            //-> Unit of Work与仓储

            //-> 领域服务


            //-> 应用服务
            Current.RegisterType<IPartAppService, PartAppService>();
            //-> 分布式服务

        }


        static void ConfigureFactories()
        {
            LoggerFactory.SetCurrent(new UniCloudLogFactory());
        }

      
        #endregion

    }
}
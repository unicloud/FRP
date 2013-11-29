#region

using System;
using Microsoft.Practices.Unity;
using UniCloud.Infrastructure.Crosscutting.Logging;
using UniCloud.Infrastructure.Crosscutting.NetFramework.Logging;

#endregion

namespace UniCloud.Infrastructure.Utilities.Container
{
    public class Configuration
    {
        public static Configuration Instance { get; private set; }

        /// <summary>
        ///     创建实例。
        /// </summary>
        /// <returns>实例。</returns>
        public static Configuration Create()
        {
            return Instance ?? (Instance = new Configuration());
        }

        /// <summary>
        ///     使用UnityContainer。
        /// </summary>
        /// <returns></returns>
        public Configuration UseAutofac()
        {
            DefaultContainer.SetContainer(new UnityContainer());
            return this;
        }

        /// <summary>
        ///     实例化对象。
        /// </summary>
        /// <typeparam name="TService">接口类型。</typeparam>
        /// <typeparam name="TImplementer">接口实现。</typeparam>
        /// <param name="lifetimeManager">对象生命周期</param>
        /// <returns></returns>
        public Configuration Register<TService, TImplementer>(LifetimeManager lifetimeManager = null)
            where TService : class
            where TImplementer : class, TService
        {
            DefaultContainer.Register<TService, TImplementer>(lifetimeManager);
            return this;
        }

        /// <summary>
        ///     实例化对象。
        /// </summary>
        /// <param name="type">注册类型。</param>
        /// <param name="lifetimeManager">对象生命周期</param>
        /// <returns></returns>
        public Configuration RegisterType(Type type, LifetimeManager lifetimeManager = null)
         
        {
            DefaultContainer.RegisterType(type, lifetimeManager);
            return this;
        }

        /// <summary>
        ///     使用日志。
        /// </summary>
        /// <returns></returns>
        public Configuration CreateLog()
        {
            LoggerFactory.SetCurrent(new UniCloudLogFactory());
            return this;
        }
    }
}
using System;
using Microsoft.Practices.Unity;

namespace UniCloud.Infrastructure.Utilities.Container
{
    public class DefaultContainer
    {
        private static IUnityContainer _container;

        /// <summary>
        /// Container单例对象。
        /// </summary>
        public static IUnityContainer Current
        {
            get
            {
                return _container;
            }
        }

        /// <summary>
        /// 创建Container。
        /// </summary>
        public static IUnityContainer CreateContainer()
        {
            _container = new UnityContainer();
            return _container;
        }

        /// <summary>
        /// 根据接口，获取对象。
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService Resolve<TService>() where TService : class
        {
            return _container.Resolve<TService>();
        }
        /// <summary>
        /// 根据接口类型，获取对象。
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static object Resolve(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
    }
}

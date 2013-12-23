using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

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
        /// 设置Container。
        /// </summary>
        /// <param name="container"></param>
        public static void SetContainer(IUnityContainer container)
        {
            _container = container;
            //SetInjectionMembers();
        }

        /// <summary>
        /// 设置依赖注入
        /// </summary>
        public static void SetInjectionMembers()
        {
            _container.AddExtension(new Interception());
        }

        public static void RegisterType(Type implementationType, LifetimeManager lifetimeManager = null, params InjectionMember[] injectionMembers)
        {
            if (lifetimeManager==null)
            {
                _container.RegisterType(implementationType,injectionMembers);
                return;
            }
            _container.RegisterType(implementationType, lifetimeManager,injectionMembers);

          
        }
        /// <summary>
        /// 实例化接口。
        /// </summary>
        /// <typeparam name="TImplementer">接口实例。</typeparam>
        /// <typeparam name="TService">接口类型。</typeparam>
        public static void Register<TService, TImplementer>(LifetimeManager lifetimeManager = null, params InjectionMember[] injectionMembers)
            where TService : class
            where TImplementer : class, TService
        {
            if (lifetimeManager==null)
            {
                _container.RegisterType<TService, TImplementer>(injectionMembers);
                return;
            }

            _container.RegisterType<TService, TImplementer>(lifetimeManager, injectionMembers);
        }
        /// <summary>
        /// 实例化接口。
        /// </summary>
        /// <typeparam name="TImplementer">接口。</typeparam>
        /// <typeparam name="TService">接口对象。</typeparam>
        public static void RegisterInstance<TService, TImplementer>(TImplementer instance)
            where TService : class
            where TImplementer : class, TService
        {
            _container.RegisterInstance(instance);
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

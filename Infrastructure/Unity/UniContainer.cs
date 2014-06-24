#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:53
// 方案：FRP
// 项目：Infrastructure
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

#endregion

namespace UniCloud.Infrastructure.Unity
{
    /// <summary>
    ///     Unity容器管理类。
    /// </summary>
    public class UniContainer
    {
        private static UniContainer _container;
        private static IUnityContainer _unityContainer;

        private UniContainer()
        {
            _unityContainer = new UnityContainer();
        }

        /// <summary>
        ///     注册的对象集合
        /// </summary>
        public IEnumerable<ContainerRegistration> Registrations
        {
            get { return _unityContainer.Registrations; }
        }

        /// <summary>
        ///     创建Unity容器实例
        /// </summary>
        /// <returns>Unity容器</returns>
        public static UniContainer Create()
        {
            return _container ?? (_container = new UniContainer());
        }

        /// <summary>
        ///     释放资源
        /// </summary>
        public static void Cleanup()
        {
            _unityContainer.Dispose();
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register<T>(params InjectionMember[] injectionMembers)
        {
            _unityContainer.RegisterType<T>(injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="TFrom">基类型</typeparam>
        /// <typeparam name="TTo">对象类型</typeparam>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register<TFrom, TTo>(params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            _unityContainer.RegisterType<TFrom, TTo>(injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="TFrom">基类型</typeparam>
        /// <typeparam name="TTo">对象类型</typeparam>
        /// <param name="lifetimeManager">生命周期</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register<TFrom, TTo>(LifetimeManager lifetimeManager,
            params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            _unityContainer.RegisterType<TFrom, TTo>(lifetimeManager, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="TFrom">基类型</typeparam>
        /// <typeparam name="TTo">对象类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register<TFrom, TTo>(string name, params InjectionMember[] injectionMembers)
            where TTo : TFrom
        {
            _unityContainer.RegisterType<TFrom, TTo>(name, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="TFrom">基类型</typeparam>
        /// <typeparam name="TTo">对象类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="lifetimeManager">生命周期</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register<TFrom, TTo>(string name, LifetimeManager lifetimeManager,
            params InjectionMember[] injectionMembers) where TTo : TFrom
        {
            _unityContainer.RegisterType<TFrom, TTo>(name, lifetimeManager, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="lifetimeManager">生命周期</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register<T>(LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            _unityContainer.RegisterType<T>(lifetimeManager, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register<T>(string name, params InjectionMember[] injectionMembers)
        {
            _unityContainer.RegisterType<T>(name, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="lifetimeManager">生命周期</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register<T>(string name, LifetimeManager lifetimeManager,
            params InjectionMember[] injectionMembers)
        {
            _unityContainer.RegisterType<T>(name, lifetimeManager, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <param name="t">对象类型</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register(Type t, params InjectionMember[] injectionMembers)
        {
            _unityContainer.RegisterType(t, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <param name="from">基类型</param>
        /// <param name="to">对象类型</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register(Type from, Type to, params InjectionMember[] injectionMembers)
        {
            _unityContainer.RegisterType(from, to, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <param name="from">基类型</param>
        /// <param name="to">对象类型</param>
        /// <param name="name">名称</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register(Type from, Type to, string name, params InjectionMember[] injectionMembers)
        {
            _unityContainer.RegisterType(from, to, name, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <param name="from">基类型</param>
        /// <param name="to">对象类型</param>
        /// <param name="lifetimeManager">生命周期</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register(Type from, Type to, LifetimeManager lifetimeManager,
            params InjectionMember[] injectionMembers)
        {
            _unityContainer.RegisterType(from, to, lifetimeManager, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <param name="t">对象类型</param>
        /// <param name="lifetimeManager">生命周期</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register(Type t, LifetimeManager lifetimeManager, params InjectionMember[] injectionMembers)
        {
            _unityContainer.RegisterType(t, lifetimeManager, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <param name="t">对象类型</param>
        /// <param name="name">名称</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register(Type t, string name, params InjectionMember[] injectionMembers)
        {
            _unityContainer.RegisterType(t, name, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <param name="t">对象类型</param>
        /// <param name="name">名称</param>
        /// <param name="lifetimeManager">生命周期</param>
        /// <param name="injectionMembers">注入参数</param>
        /// <returns></returns>
        public UniContainer Register(Type t, string name, LifetimeManager lifetimeManager,
            params InjectionMember[] injectionMembers)
        {
            _unityContainer.RegisterType(t, name, lifetimeManager, injectionMembers);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="instance">实例</param>
        /// <returns></returns>
        public UniContainer RegisterInstance<TInterface>(TInterface instance)
        {
            _unityContainer.RegisterInstance(instance);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="instance">实例</param>
        /// <param name="lifetimeManager">生命周期</param>
        /// <returns></returns>
        public UniContainer RegisterInstance<TInterface>(TInterface instance, LifetimeManager lifetimeManager)
        {
            _unityContainer.RegisterInstance(instance, lifetimeManager);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="instance">实例</param>
        /// <returns></returns>
        public UniContainer RegisterInstance<TInterface>(string name, TInterface instance)
        {
            _unityContainer.RegisterInstance(name, instance);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="instance">实例</param>
        /// <param name="lifetimeManager">生命周期</param>
        /// <returns></returns>
        public UniContainer RegisterInstance<TInterface>(string name, TInterface instance,
            LifetimeManager lifetimeManager)
        {
            _unityContainer.RegisterInstance(name, instance, lifetimeManager);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <param name="t">对象类型</param>
        /// <param name="instance">实例</param>
        /// <returns></returns>
        public UniContainer RegisterInstance(Type t, object instance)
        {
            _unityContainer.RegisterInstance(t, instance);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <param name="t">对象类型</param>
        /// <param name="instance">实例</param>
        /// <param name="lifetimeManager">生命周期</param>
        /// <returns></returns>
        public UniContainer RegisterInstance(Type t, object instance, LifetimeManager lifetimeManager)
        {
            _unityContainer.RegisterInstance(t, instance, lifetimeManager);
            return this;
        }

        /// <summary>
        ///     注册对象实例。
        /// </summary>
        /// <param name="t">对象类型</param>
        /// <param name="name">名称</param>
        /// <param name="instance">实例</param>
        /// <returns></returns>
        public UniContainer RegisterInstance(Type t, string name, object instance)
        {
            _unityContainer.RegisterInstance(t, name, instance);
            return this;
        }

        /// <summary>
        ///     获取对象实例。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>对象实例</returns>
        public static T Resolve<T>(params ResolverOverride[] overrides)
        {
            return _unityContainer.Resolve<T>(overrides);
        }

        /// <summary>
        ///     获取对象实例。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="overrides">重载</param>
        /// <returns>对象实例</returns>
        public static T Resolve<T>(string name, params ResolverOverride[] overrides)
        {
            return _unityContainer.Resolve<T>(name, overrides);
        }

        /// <summary>
        ///     获取对象实例。
        /// </summary>
        /// <param name="t">对象类型</param>
        /// <param name="overrides">重载</param>
        /// <returns>对象实例</returns>
        public static object Resolve(Type t, params ResolverOverride[] overrides)
        {
            return _unityContainer.Resolve(t, overrides);
        }

        /// <summary>
        ///     获取对象实例
        /// </summary>
        /// <param name="t">对象类型</param>
        /// <param name="name">名称</param>
        /// <param name="overrides">重载</param>
        /// <returns>对象实例</returns>
        public static object Resolve(Type t, string name, params ResolverOverride[] overrides)
        {
            return _unityContainer.Resolve(t, name, overrides);
        }

        /// <summary>
        ///     获取对象集合。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="resolverOverrides">重载</param>
        /// <returns>对象实例</returns>
        public static IEnumerable<T> ResolveAll<T>(params ResolverOverride[] resolverOverrides)
        {
            return _unityContainer.ResolveAll<T>(resolverOverrides);
        }
    }
}
#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/23，11:11
// 文件名：ConfiguratorExtension.cs
// 程序集：UniCloud.Infrastructure.Utilities
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using Microsoft.Practices.Unity;
using UniCloud.Infrastructure.Utilities.Container.Impl;
using UniCloud.Infrastructure.Utilities.Container.Interface;

#endregion

namespace UniCloud.Infrastructure.Utilities.Container
{
    public static class ConfiguratorExtension
    {
        /// <summary>
        ///     实例化对象。
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <typeparam name="TImplementer">接口实现</typeparam>
        /// <param name="configurator">IAutofacConfigurator接口对象</param>
        /// <param name="lifetimeManager">对象生命周期</param>
        /// <returns></returns>
        public static IToConfigurator Register<TService, TImplementer>(this IToConfigurator configurator,
                                                                            LifetimeManager lifetimeManager = null)
            where TService : class
            where TImplementer : class, TService
        {
            configurator.UnityContainer.RegisterType<TService, TImplementer>(lifetimeManager);
            return configurator;
        }

        /// <summary>
        ///     实例化对象。
        /// </summary>
        /// <param name="configurator">IAutofacConfigurator接口对象</param>
        /// <param name="type">注册类型。</param>
        /// <param name="lifetimeManager">对象生命周期</param>
        /// <returns></returns>
        public static IToConfigurator Register(this IToConfigurator configurator,
                                                                            Type type,
                                                                            LifetimeManager lifetimeManager = null)
        {
            configurator.UnityContainer.RegisterType(type, lifetimeManager);
            return configurator;
        }

        public static IConfigurator CreateLog(this IToConfigurator configurator)
        {
            return new LoggerConfigurator(configurator);
        }

        public static void Start(this IConfigurator configurator)
        {
            configurator.Configure();
        }
    }
}
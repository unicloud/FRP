#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/23，11:11
// 文件名：ConfiguratorBase.cs
// 程序集：UniCloud.Infrastructure.Utilities
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using Microsoft.Practices.Unity;
using UniCloud.Infrastructure.Utilities.Container.Interface;

#endregion

namespace UniCloud.Infrastructure.Utilities.Container
{
    public abstract class ConfiguratorBase : IConfigurator
    {
        private readonly IConfigurator _context;

        protected ConfiguratorBase(IConfigurator context)
        {
            _context = context;
        }

        public IConfigurator Context
        {
            get { return _context; }
        }

        /// <summary>
        ///     为保证Context上下文的唯一性，方法的返回值为IUnityContainer。
        /// </summary>
        /// <returns></returns>
        public abstract IUnityContainer Configure();
    }
}
#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/23，11:11
// 文件名：AutofacConfigurator.cs
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

namespace UniCloud.Infrastructure.Utilities.Container.Impl
{ 
    /// <summary>
    /// 设置Unity容器，先于注册方法执行。
    /// </summary>
    public class ToConfigurator : IToConfigurator
    {
        private  IUnityContainer _unityContainer;
        public IUnityContainer UnityContainer {
            get { return _unityContainer; }
        }

        /// <summary>
        /// 配置Unity容器。
        /// </summary>
        /// <returns></returns>
        public  IUnityContainer Configure()
        {
            _unityContainer = DefaultContainer.SetContainer(new UnityContainer());
            return UnityContainer;
        }
    }
}
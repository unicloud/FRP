#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/23，11:11
// 文件名：IConfigurator.cs
// 程序集：UniCloud.Infrastructure.Utilities
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using Microsoft.Practices.Unity;

#endregion

namespace UniCloud.Infrastructure.Utilities.Container.Interface
{
    /// <summary>
    ///     配置接口
    /// </summary>
    public interface IConfigurator
    {
 
        /// <summary>
        ///     配置方法。
        /// </summary>
        /// <returns></returns>
        IUnityContainer Configure();
    }
}
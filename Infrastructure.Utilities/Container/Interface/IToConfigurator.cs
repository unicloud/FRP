#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/23，11:11
// 文件名：IAutofacConfiguration.cs
// 程序集：UniCloud.Infrastructure.Utilities
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using Microsoft.Practices.Unity;

namespace UniCloud.Infrastructure.Utilities.Container.Interface
{  
    /// <summary>
    /// 设置装配置
    /// </summary>
    public interface IToConfigurator : IConfigurator
    {
        IUnityContainer UnityContainer { get; }
    }
}
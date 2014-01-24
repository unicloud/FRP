#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/01/23，17:01
// 文件名：IPartAppService.cs
// 程序集：UniCloud.Application.PartBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Application.PartBC.Services
{
    /// <summary>
    ///     应用层就部件管理的操作契约。
    ///     职责是编排操作、检查安全性，缓存，适配实体到DTO等。
    /// </summary>
    public interface IPartAppService : IDisposable
    {
    }
}
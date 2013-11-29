//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;

namespace UniCloud.Application.BaseManagementBC.Services
{
    /// <summary>
    ///     应用层就基础管理的操作契约。
    ///     职责是编排操作、检查安全性，缓存，适配实体到DTO等。
    /// </summary>
    public interface IBaseManagementAppService : IDisposable
    {
    }
}
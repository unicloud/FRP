//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System;

namespace UniCloud.Application.CommonServiceBC.Services
{
    /// <summary>
    ///     应用层就公共服务的操作契约。
    ///     职责是编排操作、检查安全性，缓存，适配实体到DTO等。
    /// </summary>
    public interface ICommonServiceAppService : IDisposable
    {
    }
}
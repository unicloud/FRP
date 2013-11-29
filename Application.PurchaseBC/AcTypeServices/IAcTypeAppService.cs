#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/21，15:11
// 文件名：IAcTypeAppService.cs
// 程序集：UniCloud.Application.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

#endregion

namespace UniCloud.Application.PurchaseBC.AcTypeServices
{
    /// <summary>
    ///     表示用于机型相关信息服务
    /// </summary>
    public interface IAcTypeAppService
    {
        /// <summary>
        ///     获取ACType
        /// </summary>
        /// <returns>所有AcTypeDTO</returns>
        IQueryable<AcTypeDTO> GetAcTypes();
    }
}
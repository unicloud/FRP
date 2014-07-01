#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，10:11
// 文件名：IMaterialAppService.cs
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

namespace UniCloud.Application.PurchaseBC.PnRegServices
{
    /// <summary>
    ///     表示用于处理部件相关信息服务接口。
    /// </summary>
    public interface IPnRegAppService
    {
        /// <summary>
        ///     部件查询。
        /// </summary>
        /// <returns>部件DTO集合</returns>
        IQueryable<PnRegDTO> GetPnRegs();
    }
}
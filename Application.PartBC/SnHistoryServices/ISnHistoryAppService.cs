#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，10:04
// 文件名：ISnHistoryAppService.cs
// 程序集：UniCloud.Application.PartBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;

#endregion

namespace UniCloud.Application.PartBC.SnHistoryServices
{
    /// <summary>
    ///     序号件装机历史服务接口。
    /// </summary>
    public interface ISnHistoryAppService
    {
        /// <summary>
        ///     获取所有序号件装机历史
        /// </summary>
        /// <returns></returns>
        IQueryable<SnHistoryDTO> GetSnHistories();
    }
}
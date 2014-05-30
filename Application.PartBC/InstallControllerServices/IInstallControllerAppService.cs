#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/07，21:04
// 文件名：IInstallControllerAppService.cs
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

namespace UniCloud.Application.PartBC.InstallControllerServices
{
    /// <summary>
    ///     装机控制服务接口。
    /// </summary>
    public interface IInstallControllerAppService
    {
        /// <summary>
        ///     获取所有装机控制
        /// </summary>
        /// <returns></returns>
        IQueryable<InstallControllerDTO> GetInstallControllers();
    }
}
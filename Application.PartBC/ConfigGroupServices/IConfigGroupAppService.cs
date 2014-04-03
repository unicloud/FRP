#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/02/25，10:02
// 文件名：IConfigGroupAppService.cs
// 程序集：UniCloud.Application.PartBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PartBC.DTO;

#endregion

namespace UniCloud.Application.PartBC.ConfigGroupServices
{
    /// <summary>
    ///     构型组服务接口。
    /// </summary>
    public interface IConfigGroupAppService
    {
        /// <summary>
        ///     获取所有 构型组
        /// </summary>
        /// <returns></returns>
        List<ConfigGroupDTO> GetConfigGroups();
    }
}
#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/13，21:04
// 文件名：IAcConfigAppService.cs
// 程序集：UniCloud.Application.PartBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PartBC.DTO;

#endregion

namespace UniCloud.Application.PartBC.AcConfigServices
{
    /// <summary>
    ///     飞机构型服务接口。
    /// </summary>
    public interface IAcConfigAppService
    {
        /// <summary>
        /// 获取所有AcConfig。
        /// </summary>
        IQueryable<AcConfigDTO> GetAcConfigs();

        /// <summary>
        /// 获取所有飞机构型
        /// </summary>
        /// <param name="contractAircraftId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<AcConfigDTO> QueryAcConfigs(int contractAircraftId, DateTime date);
    }
}
﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：IEngineAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;

#endregion

namespace UniCloud.Application.FleetPlanBC.EngineServices
{
    /// <summary>
    ///     发动机服务接口。
    /// </summary>
    public interface IEngineAppService
    {
        /// <summary>
        ///     获取所有发动机
        /// </summary>
        /// <returns></returns>
        IQueryable<EngineDTO> GetEngines();
    }
}
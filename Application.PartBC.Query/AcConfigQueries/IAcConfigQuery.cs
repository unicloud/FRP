#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/13，21:04
// 文件名：IAcConfigQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
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
using UniCloud.Domain.PartBC.Aggregates;

#endregion

namespace UniCloud.Application.PartBC.Query.AcConfigQueries
{
    public interface IAcConfigQuery
    {
        /// <summary>
        /// AcConfig查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>AcConfigDTO集合</returns>
        IQueryable<AcConfigDTO> AcConfigDTOQuery(QueryBuilder<AcConfig> query);

        /// <summary>
        /// 构型查询。
        /// </summary>
        /// <param name="contractAircraftId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<AcConfigDTO> QueryAcConfigs(int contractAircraftId, DateTime date);
    }
}
﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 17:38:24
// 文件名：IAircraftQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 17:38:24
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Application.AircraftConfigBC.Query.AircraftQueries
{
    public interface IAircraftQuery
    {/// <summary>
        ///     实际飞机查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>实际飞机DTO集合</returns>
        IQueryable<AircraftDTO> AircraftDTOQuery(
            QueryBuilder<Aircraft> query);
    }
}
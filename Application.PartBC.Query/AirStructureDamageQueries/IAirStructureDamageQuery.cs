#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 14:18:35
// 文件名：IAirStructureDamageQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 14:18:35
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AirStructureDamageAgg;

namespace UniCloud.Application.PartBC.Query.AirStructureDamageQueries
{
    /// <summary>
    /// AirStructureDamage查询接口
    /// </summary>
    public interface IAirStructureDamageQuery
    {
        /// <summary>
        /// AirStructureDamage查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>AirStructureDamageDTO集合</returns>
        IQueryable<AirStructureDamageDTO> AirStructureDamageDTOQuery(QueryBuilder<AirStructureDamage> query);
    }
}

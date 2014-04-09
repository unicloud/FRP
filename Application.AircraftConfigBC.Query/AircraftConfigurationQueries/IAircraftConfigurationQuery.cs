#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 14:01:00
// 文件名：IAircraftConfigurationQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 14:01:00
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftCabinTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg;

namespace UniCloud.Application.AircraftConfigBC.Query.AircraftConfigurationQueries
{
    public interface IAircraftConfigurationQuery
    {
        /// <summary>
        ///     飞机配置查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>飞机配置DTO集合</returns>
        IQueryable<AircraftConfigurationDTO> AircraftConfigurationDTOQuery(
            QueryBuilder<AircraftConfiguration> query);
       
    }
}

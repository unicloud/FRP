#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 15:35:18
// 文件名：IAircraftCabinTypeQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 15:35:18
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.AircraftCabinTypeAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.Query.AircraftCabinTypeQueries
{
    public interface IAircraftCabinTypeQuery
    {
        /// <summary>
        ///     飞机舱位类型查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>飞机舱位类型DTO集合</returns>
        IQueryable<AircraftCabinTypeDTO> AircraftCabinTypeDTOQuery(
            QueryBuilder<AircraftCabinType> query);
    }
}

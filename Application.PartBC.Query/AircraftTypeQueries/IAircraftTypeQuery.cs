#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：IAircraftTypeQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
#endregion

namespace UniCloud.Application.PartBC.Query.AircraftTypeQueries
{
    /// <summary>
    /// AircraftType查询接口
    /// </summary>
    public interface IAircraftTypeQuery
    {
        /// <summary>
        /// AircraftType查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>AircraftTypeDTO集合</returns>
        IQueryable<AircraftTypeDTO> AircraftTypeDTOQuery(QueryBuilder<AircraftType> query);
    }
}

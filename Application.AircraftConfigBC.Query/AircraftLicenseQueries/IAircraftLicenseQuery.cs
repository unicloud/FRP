#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 14:26:45
// 文件名：IAircraftLicenseQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 14:26:45
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg;

#endregion

namespace UniCloud.Application.AircraftConfigBC.Query.AircraftLicenseQueries
{
    public interface IAircraftLicenseQuery
    {
        /// <summary>
        ///     证照类型查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>证照类型DTO集合</returns>
        IQueryable<LicenseTypeDTO> LicenseTypeDTOQuery(
            QueryBuilder<LicenseType> query);
    }
}

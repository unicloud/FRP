#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 11:55:17
// 文件名：IBusinessLicenseQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 11:55:17
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.BusinessLicenseAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.Query.BusinessLicenseQueries
{
    /// <summary>
    /// BusinessLicense查询接口
    /// </summary>
    public interface IBusinessLicenseQuery
    {
        /// <summary>
        /// BusinessLicense查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>BusinessLicenseDTO集合</returns>
        IQueryable<BusinessLicenseDTO> BusinessLicensesQuery(QueryBuilder<BusinessLicense> query);
    }
}

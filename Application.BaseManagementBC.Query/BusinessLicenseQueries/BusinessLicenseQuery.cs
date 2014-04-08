#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 11:55:23
// 文件名：BusinessLicenseQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 11:55:23
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.BusinessLicenseAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.BaseManagementBC.Query.BusinessLicenseQueries
{
    /// <summary>
    /// BusinessLicense查询
    /// </summary>
    public class BusinessLicenseQuery: IBusinessLicenseQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public BusinessLicenseQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// BusinessLicense查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>BusinessLicenseDTO集合</returns>
        public IQueryable<BusinessLicenseDTO> BusinessLicensesQuery(QueryBuilder<BusinessLicense> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<BusinessLicense>()).Select(q => new BusinessLicenseDTO
                                                                                       {
                                                                                           Id = q.Id,
                                                                                           Description = q.Description,
                                                                                           ExpireDate = q.ExpireDate,
                                                                                           IssuedDate = q.IssuedDate,
                                                                                           IssuedUnit = q.IssuedUnit,
                                                                                           FileContent = q.FileContent,
                                                                                           FileName = q.FileName,
                                                                                           ValidMonths = q.ValidMonths,
                                                                                           Name = q.Name,
                                                                                           State = (int) q.State,
                                                                                       });
        }
    }
}

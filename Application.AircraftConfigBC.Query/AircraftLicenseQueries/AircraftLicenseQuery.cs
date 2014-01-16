#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 14:26:52
// 文件名：AircraftLicenseQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 14:26:52
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftLicenseAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.AircraftConfigBC.Query.AircraftLicenseQueries
{
    public class AircraftLicenseQuery : IAircraftLicenseQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AircraftLicenseQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     证照类型查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>证照类型DTO集合。</returns>
        public IQueryable<LicenseTypeDTO> LicenseTypeDTOQuery(
            QueryBuilder<LicenseType> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<LicenseType>()).Select(p => new LicenseTypeDTO
            {
                LicenseTypeId = p.Id,
                Name = p.Type,
                HasFile = p.HasFile,
                Description = p.Description
            });
        }

        /// <summary>
        ///     飞机证照查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>飞机证照DTO集合。</returns>
        public IQueryable<AircraftLicenseDTO> AircraftLicenseDTOQuery(
            QueryBuilder<AircraftLicense> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AircraftLicense>()).Select(p => new AircraftLicenseDTO
            {
                AircraftLicenseId = p.Id,
                Description = p.Description,
                ExpireDate = p.ExpireDate,
                IssuedDate = p.IssuedDate,
                IssuedUnit = p.IssuedUnit,
                LicenseFile = p.LicenseFile,
                FileName = p.FileName,
                LicenseTypeId = p.LicenseTypeId,
                ValidMonths = p.ValidMonths,
                Name = p.Name,
                State = (int)p.State,
            });
        }
    }
}

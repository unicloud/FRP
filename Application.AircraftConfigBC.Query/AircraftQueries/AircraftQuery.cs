#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 17:38:32
// 文件名：AircraftQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 17:38:32
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
    public class AircraftQuery : IAircraftQuery
    {
        private readonly IAircraftRepository _aircraftRepository;

        public AircraftQuery(IAircraftRepository aircraftRepository)
        {
            _aircraftRepository = aircraftRepository;
        }

        /// <summary>
        ///     实际飞机查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>实际飞机DTO集合。</returns>
        public IQueryable<AircraftDTO> AircraftDTOQuery(
            QueryBuilder<Aircraft> query)
        {
            return
                query.ApplyTo(_aircraftRepository.GetAll())
                    .Select(p => new AircraftDTO
                                 {
                                     AircraftId = p.Id,
                                     CreateDate = p.CreateDate,
                                     RegNumber = p.RegNumber,
                                     SerialNumber = p.SerialNumber,
                                     FactoryDate = p.FactoryDate,
                                     ImportDate = p.ImportDate,
                                     ExportDate = p.ExportDate,
                                     IsOperation = p.IsOperation,
                                     SeatingCapacity = p.SeatingCapacity,
                                     CarryingCapacity = p.CarryingCapacity,
                                     AircraftTypeId = p.AircraftTypeId,
                                     AircraftTypeName = p.AircraftType.Name,
                                     ManufacturerName = p.AircraftType.Manufacturer.CnName,
                                     Regional = p.AircraftType.AircraftCategory.Regional,
                                     SupplierId = p.SupplierId,
                                     SupplierName = p.Supplier.CnName,
                                     AirlinesId = p.AirlinesId,
                                     AirlinesName = p.Airlines.CnName,
                                     ImportCategoryId = p.ImportCategoryId,
                                     ImportCategoryName = p.ImportCategory.ActionType + ":" + p.ImportCategory.ActionName,
                                     AircraftLicenses = p.Licenses.Select(q => new AircraftLicenseDTO
                                                                          {
                                                                              AircraftLicenseId = q.Id,
                                                                              Description = q.Description,
                                                                              ExpireDate = q.ExpireDate,
                                                                              IssuedDate = q.IssuedDate,
                                                                              IssuedUnit = q.IssuedUnit,
                                                                              LicenseFile = q.LicenseFile,
                                                                              FileName = q.FileName,
                                                                              LicenseTypeId = q.LicenseTypeId,
                                                                              ValidMonths = q.ValidMonths,
                                                                              Name = q.Name,
                                                                              State = (int)q.State,
                                                                          }).ToList()
                                 });
        }
    }
}

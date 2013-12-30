#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 11:52:12
// 文件名：AircraftQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 11:52:12
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.AircraftQueries
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
                                     SupplierId = p.SupplierId,
                                     AirlinesId = p.AirlinesId,
                                     ImportCategoryId = p.ImportCategoryId,
                                     OperationHistories = p.OperationHistories.Select(q => new OperationHistoryDTO
                                                                                           {
                                                                                               OperationHistoryId = q.Id,
                                                                                               RegNumber = q.RegNumber,
                                                                                               TechReceiptDate = q.TechReceiptDate,
                                                                                               ReceiptDate = q.ReceiptDate,
                                                                                               StartDate = q.StartDate,
                                                                                               StopDate = q.StopDate,
                                                                                               TechDeliveryDate = q.TechDeliveryDate,
                                                                                               EndDate = q.EndDate,
                                                                                               OnHireDate = q.OnHireDate,
                                                                                               Note = q.Note,
                                                                                               AircraftId = q.AircraftId,
                                                                                               AirlinesId = q.AirlinesId,
                                                                                               ImportCategoryId = q.ImportCategoryId,
                                                                                               ExportCategoryId = q.ExportCategoryId,
                                                                                           }).ToList(),
                                     OwnershipHistories = p.OwnershipHistories.Select(q => new OwnershipHistoryDTO
                                                                                           {
                                                                                               OwnershipHistoryId = q.Id,
                                                                                               StartDate = q.StartDate,
                                                                                               EndDate = q.EndDate,
                                                                                               Status = (int)q.Status,
                                                                                               SupplierId = q.SupplierId,
                                                                                               AircraftId = q.AircraftId,
                                                                                           }).ToList(),
                                     AircraftBusinesses = p.AircraftBusinesses.Select(q => new AircraftBusinessDTO
                                                                                           {
                                                                                               AircraftBusinessId = q.Id,
                                                                                               SeatingCapacity = q.SeatingCapacity,
                                                                                               CarryingCapacity = q.CarryingCapacity,
                                                                                               StartDate = q.StartDate,
                                                                                               EndDate = q.EndDate,
                                                                                               Status = (int)q.Status,
                                                                                               AircraftId = q.AircraftId,
                                                                                               AircraftTypeId = q.AircraftTypeId,
                                                                                               ImportCategoryId = q.ImportCategoryId,
                                                                                           }).ToList()
                                 });
        }
    }
}

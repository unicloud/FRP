#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ScnQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AirBusScnAgg;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.ScnQueries
{
    /// <summary>
    /// Scn查询
    /// </summary>
    public class ScnQuery : IScnQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public ScnQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Scn查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>ScnDTO集合</returns>
        public IQueryable<ScnDTO> ScnDTOQuery(QueryBuilder<Scn> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Scn>()).Select(p => new ScnDTO
            {
                Id = p.Id,
                Title = p.Title,
                CSCNumber = p.CSCNumber,
                CheckDate = p.CheckDate,
                Cost = p.Cost,
                Description = p.Description,
                ModNumber = p.ModNumber,
                RfcNumber = p.RfcNumber,
                ScnDocumentId = p.ScnDocumentId,
                ScnDocName = p.ScnDocName,
                ScnNumber = p.ScnNumber,
                Type = (int)p.Type,
                ScnType = (int)p.ScnType,
                ValidDate = p.ValidDate,
                ReceiveDate = p.ReceiveDate,
                ScnStatus = (int)p.ScnStatus,
                AuditHistory = p.AuditHistory,
                ApplicableAircrafts = p.ApplicableAircrafts.Select(q => new ApplicableAircraftDTO
                {
                    Id = q.Id,
                    CompleteDate = q.CompleteDate,
                    Cost = q.Cost,
                    ContractAircraftId = q.ContractAircraftId,
                    ScnId = q.ScnId,
                }).ToList(),
            });
        }

        /// <summary>
        /// AirBusScn查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>AirBusScnDTO集合</returns>
        public IQueryable<AirBusScnDTO> AirBusScnDTOQuery(QueryBuilder<AirBusScn> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AirBusScn>()).Select(p => new AirBusScnDTO
            {
                Id = p.Id,
                Title = p.Title,
                CSCNumber = p.CSCNumber,
                Description = p.Description,
                ModNumber = p.ModNumber,
                ScnNumber = p.ScnNumber,
                ScnStatus = (int)p.ScnStatus,
            });
        }
    }
}

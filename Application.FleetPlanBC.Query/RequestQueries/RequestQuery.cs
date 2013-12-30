#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：RequestQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.RequestQueries
{
    public class RequestQuery : IRequestQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public RequestQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     申请查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>申请DTO集合。</returns>
        public IQueryable<RequestDTO> RequestDTOQuery(
            QueryBuilder<Request> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Request>()).Select(p => new RequestDTO
            {
                Id = p.Id,
                AirlinesId = p.AirlinesId,
                ApprovalDocId = p.ApprovalDocId,
                CaacDocNumber = p.CaacDocNumber,
                CaacNote = p.CaacNote,
                CaacDocumentId = p.CaacDocumentId,
                CreateDate = p.CreateDate,
                RaDocNumber = p.RaDocNumber,
                RaNote = p.RaNote,
                RaDocumentId = p.RaDocumentId,
                Status = (int)p.Status,
                SawsDocNumber = p.SawsDocNumber,
                SawsNote = p.SawsNote,
                SawsDocumentId = p.SawsDocumentId,
                SubmitDate = p.SubmitDate,
                Title = p.Title,
                ApprovalHistories = p.ApprovalHistories.Select(q=>new ApprovalHistoryDTO
                {
                    Id = q.Id,
                    AirlinesId = q.AirlinesId,
                    CarryingCapacity = q.CarryingCapacity,
                    ImportCategoryId = q.ImportCategoryId,
                    IsApproved = q.IsApproved,
                    Note = q.Note,
                    PlanAircraftId = q.PlanAircraftId,
                    RequestDeliverAnnualId = q.RequestDeliverAnnualId,
                    RequestDeliverMonth = q.RequestDeliverMonth,
                    RequestId = q.RequestId,
                    SeatingCapacity = q.SeatingCapacity,
                }).ToList(),

            });
        }
    }
}
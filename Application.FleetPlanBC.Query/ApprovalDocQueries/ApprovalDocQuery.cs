#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/29，17:12
// 文件名：ApprovalDocQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO.ApporvalDocDTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.ApprovalDocQueries
{
    public class ApprovalDocQuery : IApprovalDocQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public ApprovalDocQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<ApprovalDocDTO> RequestsQuery(QueryBuilder<ApprovalDoc> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<ApprovalDoc>()).Select(p => new ApprovalDocDTO
            {
                Id = p.Id,
                CaacExamineDate = p.CaacExamineDate,
                NdrcExamineDate = p.NdrcExamineDate,
                CaacApprovalNumber = p.CaacApprovalNumber,
                NdrcApprovalNumber = p.NdrcApprovalNumber,
                Status = (int) p.Status,
                Note = p.Note,
                CaacDocumentId = p.CaacDocumentId,
                NdrcDocumentId = p.NdrcDocumentId,
                CaacDocumentName = p.CaacDocumentName,
                NdrcDocumentName = p.NdrcDocumentName
            });
        }
    }
}
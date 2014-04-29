﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/29，16:12
// 文件名：RequestQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RelatedDocAgg;
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

        public IQueryable<RequestDTO> RequestsQuery(QueryBuilder<Request> query)
        {
            var relatedDocs = _unitOfWork.CreateSet<RelatedDoc>();
            var dbAirline = _unitOfWork.CreateSet<Airlines>();
            var dbImportCategory = _unitOfWork.CreateSet<ActionCategory>();
            var dbAnnaul = _unitOfWork.CreateSet<Annual>();
            var dbPlanAircraft = _unitOfWork.CreateSet<PlanAircraft>();
            return _unitOfWork.CreateSet<Request>().Select(p => new RequestDTO
            {
                Id = p.Id,
                SubmitDate = p.SubmitDate,
                IsFinished = p.IsFinished,
                Title = p.Title,
                CreateDate = p.CreateDate,
                CaacDocNumber = p.CaacDocNumber,
                Status = (int)p.Status,
                Note = p.Note,
                ApprovalDocId = p.ApprovalDocId,
                CaacDocumentId = p.CaacDocumentId,
                CaacDocumentName = p.CaacDocumentName,
                AirlinesId = p.AirlinesId,
                AirlinesName = dbAirline.FirstOrDefault(c => c.Id == p.AirlinesId).CnShortName,
                ApprovalHistories = p.ApprovalHistories.Select(c => new ApprovalHistoryDTO
                {
                    Id = c.Id,
                    IsApproved = c.IsApproved,
                    SeatingCapacity = c.SeatingCapacity,
                    CarryingCapacity = c.CarryingCapacity,
                    RequestDeliverMonth = c.RequestDeliverMonth,
                    Note = c.Note,
                    RequestId = c.RequestId,
                    PlanAircraftId = c.PlanAircraftId,
                    ImportCategoryId = c.ImportCategoryId,
                    AircraftRegional = c.PlanAircraft.AircraftType.AircraftCategory.Regional,
                    AircraftType = c.PlanAircraft.AircraftType.Name,
                    ImportCategoryName =
                        dbImportCategory.FirstOrDefault(a => a.Id == c.ImportCategoryId).ActionType + "-"
                        + dbImportCategory.FirstOrDefault(a => a.Id == c.ImportCategoryId).ActionName,
                    RequestDeliverAnnualId = c.RequestDeliverAnnualId,
                    RequestDeliverAnnualName = dbAnnaul.FirstOrDefault(a => a.Id == c.RequestDeliverAnnualId).Year,
                    AirlinesId = c.AirlinesId,
                    AirlineName = dbAirline.FirstOrDefault(a => a.Id == c.AirlinesId).CnShortName,
                    PlanAircraftStatus = (int)dbPlanAircraft.FirstOrDefault(a => a.Id == c.PlanAircraftId).Status,
                }).ToList(),
                RelatedDocs = relatedDocs.Where(r => r.SourceId == p.Id).Select(r => new RelatedDocDTO
                {
                    Id = r.Id,
                    SourceId = r.SourceId,
                    DocumentId = r.DocumentId,
                    DocumentName = r.DocumentName
                }).ToList(),
            });
        }

        public IQueryable<ApprovalRequestDTO> ApprovalRequestsQuery(QueryBuilder<Request> query)
        {
            var dbAirline = _unitOfWork.CreateSet<Airlines>();
            var dbImportCategory = _unitOfWork.CreateSet<ActionCategory>();
            var dbAnnaul = _unitOfWork.CreateSet<Annual>();
            var dbPlanAircraft = _unitOfWork.CreateSet<PlanAircraft>();
            var dbApproval = _unitOfWork.CreateSet<ApprovalDoc>();
            return _unitOfWork.CreateSet<Request>().Select(p => new ApprovalRequestDTO
            {
                Id = p.Id,
                Title = p.Title,
                CaacDocNumber = p.CaacDocNumber,
                Status = (int)p.Status,
                ApprovalDocId = p.ApprovalDocId,
                CaacRequestDocumentId = p.CaacDocumentId,
                CaacRequestDocumentName = p.CaacDocumentName,
                CaacExamineDate = dbApproval.FirstOrDefault(c => c.Id == p.ApprovalDocId).CaacExamineDate,
                NdrcExamineDate = dbApproval.FirstOrDefault(c => c.Id == p.ApprovalDocId).NdrcExamineDate,
                CaacApprovalNumber = dbApproval.FirstOrDefault(c => c.Id == p.ApprovalDocId).CaacApprovalNumber,
                NdrcApprovalNumber = dbApproval.FirstOrDefault(c => c.Id == p.ApprovalDocId).NdrcApprovalNumber,
                CaacDocumentId = dbApproval.FirstOrDefault(c => c.Id == p.ApprovalDocId).CaacDocumentId,
                NdrcDocumentId = dbApproval.FirstOrDefault(c => c.Id == p.ApprovalDocId).NdrcDocumentId,
                CaacDocumentName = dbApproval.FirstOrDefault(c => c.Id == p.ApprovalDocId).CaacDocumentName,
                NdrcDocumentName = dbApproval.FirstOrDefault(c => c.Id == p.ApprovalDocId).NdrcDocumentName,
                ApprovalHistories = p.ApprovalHistories.Select(c => new ApprovalHistoryDTO
                {
                    Id = c.Id,
                    IsApproved = c.IsApproved,
                    SeatingCapacity = c.SeatingCapacity,
                    CarryingCapacity = c.CarryingCapacity,
                    RequestDeliverMonth = c.RequestDeliverMonth,
                    Note = c.Note,
                    RequestId = c.RequestId,
                    PlanAircraftId = c.PlanAircraftId,
                    ImportCategoryId = c.ImportCategoryId,
                    AircraftRegional = c.PlanAircraft.AircraftType.AircraftCategory.Regional,
                    AircraftType = c.PlanAircraft.AircraftType.Name,
                    ImportCategoryName =
                        dbImportCategory.FirstOrDefault(a => a.Id == c.ImportCategoryId).ActionType + "-"
                        + dbImportCategory.FirstOrDefault(a => a.Id == c.ImportCategoryId).ActionName,
                    RequestDeliverAnnualId = c.RequestDeliverAnnualId,
                    RequestDeliverAnnualName = dbAnnaul.FirstOrDefault(a => a.Id == c.RequestDeliverAnnualId).Year,
                    AirlinesId = c.AirlinesId,
                    AirlineName = dbAirline.FirstOrDefault(a => a.Id == c.AirlinesId).CnShortName,
                    PlanAircraftStatus = (int)dbPlanAircraft.FirstOrDefault(a => a.Id == c.PlanAircraftId).Status,
                }).ToList()
            });
        }
    }
}

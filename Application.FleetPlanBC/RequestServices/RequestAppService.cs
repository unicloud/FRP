#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/12，16:12
// 文件名：ContractAircraftAppService.cs
// 程序集：UniCloud.Application.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.RequestQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.RequestServices
{
    public class RequestAppService : IRequestAppService
    {
        private readonly IRequestQuery _requestQuery;
        private readonly IRequestRepository _requestRepository;
        private readonly IPlanAircraftRepository _planAircraftRepository;
        public RequestAppService(IRequestQuery requestQuery, IRequestRepository requestRepository, IPlanAircraftRepository planAircraftRepository)
        {
            _requestQuery = requestQuery;
            _requestRepository = requestRepository;
            _planAircraftRepository = planAircraftRepository;
        }

        public IQueryable<RequestDTO> GetRequests()
        {
            return _requestQuery.RequestsQuery(new QueryBuilder<Request>());
        }

        /// <summary>
        ///     新增申请
        /// </summary>
        /// <param name="request">申请</param>
        [Insert(typeof (RequestDTO))]
        public void InsertRequest(RequestDTO request)
        {
            if (request == null)
            {
                throw new Exception("申请不能为空");
            }
            //新申请
            var newRequest = RequestFactory.CreateRequest(request.SubmitDate, request.Title, request.RaDocNumber,
                request.SawsDocNumber,
                request.CaacDocNumber, request.Status, request.CaacNote, request.RaNote, request.SawsNote,
                request.RaDocumentName, request.SawsDocumentName, request.CaacDocumentName, request.RaDocumentId,
                request.SawsDocumentId,
                request.CaacDocumentId, Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F"));
            request.ApprovalHistories.ToList().ForEach(p => newRequest.AddNewApprovalHistory(p.SeatingCapacity,
                p.CarryingCapacity,
                p.RequestDeliverMonth, p.Note, p.RequestId,
                p.PlanAircraftId, p.ImportCategoryId, p.RequestDeliverAnnualId,
                Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F")));
            _requestRepository.Add(newRequest);
        }

        /// <summary>
        ///     修改申请
        /// </summary>
        /// <param name="request"></param>
        [Update(typeof (RequestDTO))]
        public void ModifyRequest(RequestDTO request)
        {
            if (request == null)
            {
                throw new Exception("申请不能为空");
            }
            var pesistRequest = _requestRepository.Get(request.Id);
            if (pesistRequest == null)
            {
                throw new Exception("找不到需要更新的申请");
            }
            //判断，如果两字段不相等，则更新
            if (pesistRequest.SubmitDate != request.SubmitDate)
            {
                pesistRequest.SetSubmitDate(request.SubmitDate);
            }
            if (pesistRequest.Title != request.Title)
            {
                pesistRequest.SetTitle(request.Title);
            }
            if (pesistRequest.RaDocNumber != request.RaDocNumber)
            {
                pesistRequest.SetRaDocNumber(request.RaDocNumber);
            }
            if (pesistRequest.SawsDocNumber != request.SawsDocNumber)
            {
                pesistRequest.SetSawsDocNumber(request.SawsDocNumber);
            }
            if (pesistRequest.CaacDocNumber != request.CaacDocNumber)
            {
                pesistRequest.SetCaacDocNumber(request.CaacDocNumber);
            }
            if (pesistRequest.Status != (RequestStatus) request.Status)
            {
                pesistRequest.SetRequestStatus((RequestStatus) request.Status);
            }
            if (pesistRequest.CaacNote != request.CaacNote)
            {
                pesistRequest.SetCaacNote(request.CaacNote);
            }
            if (pesistRequest.RaNote != request.RaNote)
            {
                pesistRequest.SetRaNote(request.RaNote);
            }
            if (pesistRequest.SawsNote != request.SawsNote)
            {
                pesistRequest.SetSawsNote(request.SawsNote);
            }
            if (pesistRequest.RaDocumentId != request.RaDocumentId)
            {
                pesistRequest.SetRaDocument(request.RaDocumentId, request.RaDocumentName);
            }
            if (pesistRequest.SawsDocumentId != request.SawsDocumentId)
            {
                pesistRequest.SetSawsDocument(request.SawsDocumentId, request.SawsDocumentName);
            }
            if (pesistRequest.CaacDocumentId != request.CaacDocumentId)
            {
                pesistRequest.SetCaacDocument(request.CaacDocumentId, request.CaacDocumentName);
            }
            if (pesistRequest.ApprovalDocId != request.ApprovalDocId)
            {
                pesistRequest.SetApprovalDoc(request.ApprovalDocId);
            }
            DataHelper.DetailHandle(request.ApprovalHistories.ToArray(), pesistRequest.ApprovalHistories.ToArray(),
                c => c.Id, c => c.Id, c => InsertApprovalHistory(pesistRequest, c), ModifyApprovalHistory,
                DeleteApprovalHistory);
            _requestRepository.Modify(pesistRequest);
        }

        /// <summary>
        ///     删除申请
        /// </summary>
        /// <param name="request"></param>
        [Delete(typeof (RequestDTO))]
        public void DeleteRequest(RequestDTO request)
        {
            if (request == null)
            {
                throw new Exception("申请不能为空");
            }
            var pesistRequest = _requestRepository.Get(request.Id);
            if (pesistRequest == null)
            {
                throw new Exception("找不到需要删除的申请");
            }
            _requestRepository.Remove(pesistRequest);
        }

        /// <summary>
        ///     新增批文历史
        /// </summary>
        /// <param name="request"></param>
        /// <param name="approvalHistory"></param>
        private void InsertApprovalHistory(Request request, ApprovalHistoryDTO approvalHistory)
        {
            request.AddNewApprovalHistory(approvalHistory.SeatingCapacity,
                approvalHistory.CarryingCapacity,
                approvalHistory.RequestDeliverMonth, approvalHistory.Note, approvalHistory.RequestId,
                approvalHistory.PlanAircraftId, approvalHistory.ImportCategoryId, approvalHistory.RequestDeliverAnnualId,
                Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F"));
        }

        /// <summary>
        ///     修改申请明细
        /// </summary>
        /// <param name="approvalHistory"></param>
        /// <param name="pesistApprovalHistory"></param>
        private void ModifyApprovalHistory(ApprovalHistoryDTO approvalHistory, ApprovalHistory pesistApprovalHistory)
        {
            if (pesistApprovalHistory.SeatingCapacity != approvalHistory.SeatingCapacity)
            {
                pesistApprovalHistory.SetSeatingCapacity(approvalHistory.SeatingCapacity);
            }
            if (pesistApprovalHistory.CarryingCapacity != approvalHistory.CarryingCapacity)
            {
                pesistApprovalHistory.SetCarryingCapacity(approvalHistory.CarryingCapacity);
            }
            if (pesistApprovalHistory.RequestDeliverMonth != approvalHistory.RequestDeliverMonth)
            {
                pesistApprovalHistory.SetDeliverDate(approvalHistory.RequestDeliverMonth);
            }
            if (pesistApprovalHistory.Note != approvalHistory.Note)
            {
                pesistApprovalHistory.SetNote(approvalHistory.Note);
            }
            if (pesistApprovalHistory.RequestId != approvalHistory.RequestId)
            {
                pesistApprovalHistory.SetRequest(approvalHistory.RequestId);
            }
            if (pesistApprovalHistory.RequestId != approvalHistory.RequestId)
            {
                pesistApprovalHistory.SetRequest(approvalHistory.RequestId);
            }
            if (pesistApprovalHistory.PlanAircraftId != approvalHistory.PlanAircraftId)
            {
                pesistApprovalHistory.SetPlanAircraft(approvalHistory.PlanAircraftId);
            }
            if (pesistApprovalHistory.ImportCategoryId != approvalHistory.ImportCategoryId)
            {
                pesistApprovalHistory.SetImportCategory(approvalHistory.ImportCategoryId);
            }
            if (pesistApprovalHistory.RequestDeliverAnnualId != approvalHistory.RequestDeliverAnnualId)
            {
                pesistApprovalHistory.SetDeliverDate(approvalHistory.RequestDeliverAnnualId);
            }
            if (pesistApprovalHistory.IsApproved != approvalHistory.IsApproved)
            {
                pesistApprovalHistory.SetIsApproved(approvalHistory.IsApproved);
            }
            var persistPlanAircraft = _planAircraftRepository.Get(approvalHistory.PlanAircraftId);
            if (persistPlanAircraft.Status != (ManageStatus)approvalHistory.PlanAircraftStatus)
            {
                if (pesistApprovalHistory.PlanAircraft.Status==ManageStatus.申请
                    &&approvalHistory.PlanAircraftStatus==(int)ManageStatus.批文)
                {
                    pesistApprovalHistory.PlanAircraft.SetManageStatus(ManageStatus.批文);//修改计划飞机为批文状态
                }
                if (pesistApprovalHistory.PlanAircraft.Status == ManageStatus.批文
                    && approvalHistory.PlanAircraftStatus == (int)ManageStatus.申请)
                {
                    pesistApprovalHistory.PlanAircraft.SetManageStatus(ManageStatus.申请);
                }
             
            }
           
         
        }

        /// <summary>
        ///     删除申请明细
        /// </summary>
        /// <param name="approvalHistory">申请明细</param>
        private void DeleteApprovalHistory(ApprovalHistory approvalHistory)
        {
            _requestRepository.DelApprovalHistory(approvalHistory);
        }
    }
}
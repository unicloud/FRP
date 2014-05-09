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
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.ApprovalDocQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.ApprovalDocServices
{
    [LogAOP]
    public class ApprovalDocAppService : ContextBoundObject, IApprovalDocAppService
    {
        private readonly IApprovalDocQuery _approvalDocQuery;
        private readonly IApprovalDocRepository _approvalDocRepository;

        public ApprovalDocAppService(IApprovalDocQuery approvalDocQuery, IApprovalDocRepository approvalDocRepository)
        {
            _approvalDocQuery = approvalDocQuery;
            _approvalDocRepository = approvalDocRepository;
        }

        public IQueryable<ApprovalDocDTO> GetApprovalDocs()
        {
            return _approvalDocQuery.ApprovalDocsQuery(new QueryBuilder<ApprovalDoc>());
        }

        [Insert(typeof (ApprovalDocDTO))]
        public void InsertApprovalDoc(ApprovalDocDTO approvalDoc)
        {
            if (approvalDoc == null)
            {
                throw new Exception("批文不能为空");
            }
            //新批文申请
            var newApprovalDoc = ApprovalDocFactory.CreateApprovalDoc(approvalDoc.Id, approvalDoc.CaacExamineDate,
                approvalDoc.NdrcExamineDate, approvalDoc.CaacApprovalNumber,
                approvalDoc.NdrcApprovalNumber, approvalDoc.Status, approvalDoc.Note,
                approvalDoc.CaacDocumentName, approvalDoc.NdrcDocumentName, approvalDoc.CaacDocumentId,
                approvalDoc.NdrcDocumentId);
            _approvalDocRepository.Add(newApprovalDoc);
        }

        [Update(typeof (ApprovalDocDTO))]
        public void ModifyApprovalDoc(ApprovalDocDTO approvalDoc)
        {
            if (approvalDoc == null)
            {
                throw new Exception("批文不能为空");
            }
            var pesistApprovalDoc = _approvalDocRepository.Get(approvalDoc.Id);
            if (pesistApprovalDoc == null)
            {
                throw new Exception("找不到需要更新的批文");
            }
            //判断，如果两字段不相等，则更新
            if (pesistApprovalDoc.CaacExamineDate != approvalDoc.CaacExamineDate)
            {
                pesistApprovalDoc.SetCaacExamineDate(approvalDoc.CaacExamineDate);
            }
            if (pesistApprovalDoc.NdrcExamineDate != approvalDoc.NdrcExamineDate)
            {
                pesistApprovalDoc.SetNdrcExamineDate(approvalDoc.NdrcExamineDate);
            }
            if (pesistApprovalDoc.CaacApprovalNumber != approvalDoc.CaacApprovalNumber)
            {
                pesistApprovalDoc.SetCaacApprovalNumber(approvalDoc.CaacApprovalNumber);
            }
            if (pesistApprovalDoc.NdrcApprovalNumber != approvalDoc.NdrcApprovalNumber)
            {
                pesistApprovalDoc.SetNdrcApprovalNumber(approvalDoc.NdrcApprovalNumber);
            }
            if (pesistApprovalDoc.Status != (OperationStatus) (approvalDoc.Status))
            {
                pesistApprovalDoc.SetOperationStatus((OperationStatus) (approvalDoc.Status));
            }
            if (pesistApprovalDoc.Note != approvalDoc.Note)
            {
                pesistApprovalDoc.SetNote(approvalDoc.Note);
            }
            if (pesistApprovalDoc.CaacDocumentId != approvalDoc.CaacDocumentId)
            {
                pesistApprovalDoc.SetCaacDocument(approvalDoc.CaacDocumentId, approvalDoc.CaacDocumentName);
            }
            if (pesistApprovalDoc.NdrcDocumentId != approvalDoc.NdrcDocumentId)
            {
                pesistApprovalDoc.SetNdrcDocument(approvalDoc.NdrcDocumentId, approvalDoc.NdrcDocumentName);
            }
            _approvalDocRepository.Modify(pesistApprovalDoc);
        }

        [Delete(typeof (ApprovalDocDTO))]
        public void DeleteApprovalDoc(ApprovalDocDTO approvalDoc)
        {
            if (approvalDoc == null)
            {
                throw new Exception("批文不能为空");
            }
            var pesistApprovalDoc = _approvalDocRepository.Get(approvalDoc.Id);
            if (pesistApprovalDoc == null)
            {
                throw new Exception("找不到需要删除的批文");
            }
            _approvalDocRepository.Remove(pesistApprovalDoc);
        }
    }
}
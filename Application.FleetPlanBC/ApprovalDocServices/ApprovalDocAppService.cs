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
using UniCloud.Application.FleetPlanBC.DTO.ApporvalDocDTO;
using UniCloud.Application.FleetPlanBC.Query.ApprovalDocQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.ApprovalDocServices
{
    public class ApprovalDocAppService : IApprovalDocAppService
    {
        private readonly IApprovalDocQuery _approvalDocQuery;

        public ApprovalDocAppService(IApprovalDocQuery approvalDocQuery)
        {
            _approvalDocQuery = approvalDocQuery;
        }

        public IQueryable<ApprovalDocDTO> GetApprovalDocs()
        {
            return _approvalDocQuery.RequestsQuery(new QueryBuilder<ApprovalDoc>());
        }

        [Insert(typeof (ApprovalDocDTO))]
        public void InsertApprovalDoc(ApprovalDocDTO approvalDoc)
        {
            if (approvalDoc == null)
            {
                throw new Exception("批文不能为空");
            }
            var newapprovalDoc = ApprovalDocFactory.CreateApprovalDoc(approvalDoc.CaacExamineDate,
                approvalDoc.NdrcExamineDate, approvalDoc.CaacApprovalNumber,
                approvalDoc.NdrcApprovalNumber, approvalDoc.Status, approvalDoc.Note,
                approvalDoc.CaacDocumentName, approvalDoc.NdrcDocumentName, approvalDoc.CaacDocumentId,
                approvalDoc.NdrcDocumentId);
        }

        [Update(typeof (ApprovalDocDTO))]
        public void ModifyApprovalDoc(ApprovalDocDTO approvalDoc)
        {
            if (approvalDoc == null)
            {
                throw new Exception("批文不能为空");
            }
        }

        [Delete(typeof (ApprovalDocDTO))]
        public void DeleteApprovalDoc(ApprovalDocDTO approvalDoc)
        {
            if (approvalDoc == null)
            {
                throw new Exception("批文不能为空");
            }
        }
    }
}
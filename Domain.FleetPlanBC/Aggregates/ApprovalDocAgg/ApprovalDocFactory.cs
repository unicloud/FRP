#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:27:10
// 文件名：ApprovalDocFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg
{
    /// <summary>
    ///     批文工厂
    /// </summary>
    public static class ApprovalDocFactory
    {
        /// <summary>
        ///     创建批文文档
        /// </summary>
        /// <returns>批文</returns>
        public static ApprovalDoc CreateApprovalDoc(Guid key,DateTime? caacExamineDate, DateTime? ndrcExamineDate,
            string caacApprovalNumber, string ndrcApprovalNumber,
            int status, string note, string caacDocumentName, string ndrcDocumentName, Guid? caacDocumentId,
            Guid? ndrcDocumentId)
        {
            var approvalDoc = new ApprovalDoc();
            approvalDoc.SetCaacExamineDate(caacExamineDate);
            approvalDoc.SetNdrcExamineDate(ndrcExamineDate);
            approvalDoc.SetCaacApprovalNumber(caacApprovalNumber);
            approvalDoc.SetNdrcApprovalNumber(ndrcApprovalNumber);
            approvalDoc.SetOperationStatus((OperationStatus) status);
            approvalDoc.SetNote(note);
            approvalDoc.SetCaacDocument(caacDocumentId, caacDocumentName);
            approvalDoc.SetNdrcDocument(ndrcDocumentId, ndrcDocumentName);
            approvalDoc.SetKey(key);
            approvalDoc.SetDispatchUnit(Guid.Parse("31A9DE51-C207-4A73-919C-21521F17FEF9"));
            return approvalDoc;
        }
    }
}
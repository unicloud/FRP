#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:30:10
// 文件名：RequestFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;
using UniCloud.Domain.FleetPlanBC.Enums;

namespace UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg
{
    /// <summary>
    ///     申请工厂
    /// </summary>
    public static class RequestFactory
    {
        /// <summary>
        ///     创建申请
        /// </summary>
        /// <returns>申请</returns>
        public static Request CreateRequest(DateTime? submitDate, string title, string raDocNumber,
                                            string sawsDocNumber, string caacDocNumber, int status, string caacNote, string raNote, 
            string sawsNote, string raDocumentName, string sawsDocumentName, string caacDocumentName,
          Guid? raDocumentId, Guid? sawsDocumentId, Guid? caacDocumentId, Guid airlinesId)
        {
            var request = new Request
            {
                CreateDate = DateTime.Now,
                SubmitDate = submitDate,
            };
            request.SetTitle(title);
            request.SetRaDocNumber(raDocNumber);
            request.SetSawsDocNumber(sawsDocNumber);
            request.SetCaacDocNumber(caacDocNumber);
            request.SetRequestStatus((RequestStatus)status);
            request.SetCaacNote(caacNote);
            request.SetRaNote(raNote);
            request.SetSawsNote(sawsNote);
            request.SetRaDocument(raDocumentId,raDocumentName);
            request.SetSawsDocument(sawsDocumentId,sawsDocumentName);
            request.SetCaacDocument(caacDocumentId,caacDocumentName);
            return request;
        }
    }
}

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

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

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
        /// <param name="submitDate">提交日期</param>
        /// <param name="title">标题</param>
        /// <param name="caacDocNumber">民航局申请文号</param>
        /// <param name="status">状态</param>
        /// <param name="note">民航局申请跟踪备忘录</param>
        /// <param name="caacDocumentName">民航局文档名称</param>
        /// <param name="caacDocumentId">民航局文档主键</param>
        /// <param name="airlinesId"></param>
        /// <returns></returns>
        public static Request CreateRequest(DateTime? submitDate, string title,
            string caacDocNumber, int status, string note,string caacDocumentName,
             Guid caacDocumentId, Guid airlinesId)
        {
            var request = new Request
            {
                CreateDate = DateTime.Now,
                SubmitDate = submitDate,
            };
            request.GenerateNewIdentity();
            request.SetTitle(title);
            request.SetCaacDocNumber(caacDocNumber);
            request.SetRequestStatus((RequestStatus) status);
            request.SetNote(note);
            request.SetCaacDocument(caacDocumentId, caacDocumentName);
            request.SetAirlines(airlinesId);
            return request;
        }
    }
}
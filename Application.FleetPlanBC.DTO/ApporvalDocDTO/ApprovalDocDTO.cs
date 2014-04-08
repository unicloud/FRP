#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:28:14
// 文件名：ApprovalDocDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 批文
    /// </summary>
    [DataServiceKey("Id")]
    public class ApprovalDocDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     民航局审批日期
        /// </summary>
        public DateTime? CaacExamineDate { get;  set; }

        /// <summary>
        ///  发改委审批日期
        /// </summary>
        public DateTime? NdrcExamineDate { get; set; }
        /// <summary>
        ///     民航局批文文号
        /// </summary>
        public string CaacApprovalNumber { get;  set; }

        /// <summary>
        ///     发改委批文文号
        /// </summary>
        public string NdrcApprovalNumber { get;  set; }

        /// <summary>
        ///     处理状态
        /// </summary>
        public int Status { get;  set; }

        /// <summary>
        ///     评审意见
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     民航局批文文档名称
        /// </summary>
        public string CaacDocumentName { get;  set; }

        /// <summary>
        ///     发改委批文文档
        /// </summary>
        public string NdrcDocumentName { get;  set; }

        /// <summary>
        ///     民航局批文文档
        /// </summary>
        public Guid? CaacDocumentId { get;  set; }

        /// <summary>
        ///     发改委批文文档
        /// </summary>
        public Guid? NdrcDocumentId { get;  set; }
    }
}

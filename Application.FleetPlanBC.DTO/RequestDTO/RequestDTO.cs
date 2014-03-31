﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:33:34
// 文件名：RequestDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    ///     申请
    /// </summary>
    [DataServiceKey("Id")]
    public class RequestDTO
    {
        #region 私有字段

        /// <summary>
        ///     申请行集合
        /// </summary>
        private List<ApprovalHistoryDTO> _approvalHistories;

        #endregion

        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     提交日期
        /// </summary>
        public DateTime? SubmitDate { get; set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        ///     申请标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     地方局申请文号
        /// </summary>
        public string RaDocNumber { get; set; }

        /// <summary>
        ///     监管局申请文号
        /// </summary>
        public string SawsDocNumber { get; set; }

        /// <summary>
        ///     民航局申请文号
        /// </summary>
        public string CaacDocNumber { get; set; }

        /// <summary>
        ///     民航局申请状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     民航局审批意见
        /// </summary>
        public string CaacNote { get; set; }

        /// <summary>
        ///     地方局审批意见
        /// </summary>
        public string RaNote { get; set; }

        /// <summary>
        ///     监管局审批意见
        /// </summary>
        public string SawsNote { get; set; }

        /// <summary>
        ///     民航局批文文档外键
        /// </summary>
        public Guid? ApprovalDocId { get; set; }

        /// <summary>
        ///     地方局申请文档Id
        /// </summary>
        public Guid? RaDocumentId { get; set; }

        /// <summary>
        ///     地方局批文文档名称
        /// </summary>
        public string RaDocumentName { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     监管局申请文档Id
        /// </summary>
        public Guid? SawsDocumentId { get; set; }

        /// <summary>
        ///     监管局申请文档名称
        /// </summary>
        public string SawsDocumentName { get; set; }

        /// <summary>
        ///     民航局申请文档Id
        /// </summary>
        public Guid? CaacDocumentId { get; set; }

        /// <summary>
        ///     民航局申请文档名称
        /// </summary>
        public string CaacDocumentName { get; set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; set; }

        /// <summary>
        ///     航空公司名称
        /// </summary>
        public string AirlinesName { get; set; }

        public virtual List<ApprovalHistoryDTO> ApprovalHistories
        {
            get { return _approvalHistories ?? new List<ApprovalHistoryDTO>(); }
            set { _approvalHistories = value; }
        }

        #endregion
    }
}
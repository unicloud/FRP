using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 已有申请的批文
    /// </summary>
        [DataServiceKey("Id")]
   public  class ApprovalRequestDTO
    {

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     民航局审批日期
        /// </summary>
        public DateTime? CaacExamineDate { get; set; }

        /// <summary>
        ///  发改委审批日期
        /// </summary>
        public DateTime? NdrcExamineDate { get; set; }
        /// <summary>
        ///     民航局批文文号
        /// </summary>
        public string CaacApprovalNumber { get; set; }

        /// <summary>
        ///     发改委批文文号
        /// </summary>
        public string NdrcApprovalNumber { get; set; }

        /// <summary>
        ///     评审意见
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     民航局批文文档名称
        /// </summary>
        public string CaacDocumentName { get; set; }

        /// <summary>
        ///     发改委批文文档
        /// </summary>
        public string NdrcDocumentName { get; set; }

        /// <summary>
        ///     民航局批文文档
        /// </summary>
        public Guid? CaacDocumentId { get; set; }

        /// <summary>
        ///     发改委批文文档
        /// </summary>
        public Guid? NdrcDocumentId { get; set; }

        /// <summary>
        ///     申请标题
        /// </summary>
        public string Title { get; set; }

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
        public Guid? CaacRequestDocumentId { get; set; }

        /// <summary>
        ///     民航局申请文档名称
        /// </summary>
        public string CaacRequestDocumentName { get; set; }

        /// <summary>
        ///     申请行集合
        /// </summary>
        private List<ApprovalHistoryDTO> _approvalHistories;

        public virtual List<ApprovalHistoryDTO> ApprovalHistories
        {
            get { return _approvalHistories ?? new List<ApprovalHistoryDTO>(); }
            set { _approvalHistories = value; }
        }

    }
}

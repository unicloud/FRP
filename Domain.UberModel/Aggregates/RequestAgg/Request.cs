#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 16:58:28
// 文件名：Request
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.UberModel.Aggregates.AirlinesAgg;
using UniCloud.Domain.UberModel.Aggregates.ApprovalDocAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.RequestAgg
{
    /// <summary>
    ///     申请聚合根
    /// </summary>
    public class Request : EntityGuid
    {
        #region 私有字段

        private HashSet<ApprovalHistory> _approvalHistories;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Request()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     提交日期
        /// </summary>
        public DateTime? SubmitDate { get; internal set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsFinished { get; private set; }

        /// <summary>
        ///     申请标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     民航局申请文号
        /// </summary>
        public string CaacDocNumber { get; private set; }

        /// <summary>
        ///     民航局申请状态
        /// </summary>
        public RequestStatus Status { get; private set; }

        /// <summary>
        ///     申请跟踪说明备忘
        /// </summary>
        public string Note { get; private set; }

        /// <summary>
        ///     民航局申请文档名称
        /// </summary>
        public string CaacDocumentName { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     民航局批文文档外键
        /// </summary>
        public Guid? ApprovalDocId { get; private set; }

        /// <summary>
        ///     民航局申请文档Id
        /// </summary>
        public Guid? CaacDocumentId { get; private set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     批文
        /// </summary>
        public virtual ApprovalDoc ApprovalDoc { get; set; }


        /// <summary>
        ///     航空公司
        /// </summary>
        public virtual Airlines Airlines { get; set; }

        /// <summary>
        ///     审批历史集合（申请明细集合）
        /// </summary>
        public virtual ICollection<ApprovalHistory> ApprovalHistories
        {
            get { return _approvalHistories ?? (_approvalHistories = new HashSet<ApprovalHistory>()); }
            set { _approvalHistories = new HashSet<ApprovalHistory>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置申请标题
        /// </summary>
        /// <param name="title">申请标题</param>
        public void SetTitle(string title)
        {
            Title = title;
        }

        /// <summary>
        ///     设置民航局申请文号
        /// </summary>
        /// <param name="caacDocNumber">民航局申请文号</param>
        public void SetCaacDocNumber(string caacDocNumber)
        {
            CaacDocNumber = caacDocNumber;
        }

        /// <summary>
        ///     设置民航局申请状态
        /// </summary>
        /// <param name="status">民航局申请状态</param>
        public void SetRequestStatus(RequestStatus status)
        {
            switch (status)
            {
                case RequestStatus.草稿:
                    Status = RequestStatus.草稿;
                    break;
                case RequestStatus.待审核:
                    Status = RequestStatus.待审核;
                    break;
                case RequestStatus.已审核:
                    Status = RequestStatus.已审核;
                    break;
                case RequestStatus.已提交:
                    Status = RequestStatus.已提交;
                    break;
                case RequestStatus.已审批:
                    Status = RequestStatus.已审批;
                    IsFinished = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        /// <summary>
        ///     设置民航局申请跟踪备忘录
        /// </summary>
        /// <param name="note"></param>
        public void SetNote(string note)
        {
            Note = note;
        }

        /// <summary>
        ///     设置民航局批文文档
        /// </summary>
        /// <param name="approvalDocId">民航局批文文档</param>
        public void SetApprovalDoc(Guid? approvalDocId)
        {
            ApprovalDocId = approvalDocId;
        }

        /// <summary>
        ///     设置民航局申请文档
        /// </summary>
        /// <param name="caacDocumentId">民航局申请文档</param>
        /// <param name="caacDocumentName">民航局申请文档名称</param>
        public void SetCaacDocument(Guid? caacDocumentId, string caacDocumentName)
        {
            CaacDocumentId = caacDocumentId;
            CaacDocumentName = caacDocumentName;
        }

        /// <summary>
        ///     设置航空公司
        /// </summary>
        /// <param name="airlinesId">航空公司</param>
        public void SetAirlines(Guid airlinesId)
        {
            if (airlinesId == null)
            {
                throw new ArgumentException("航空公司Id参数为空！");
            }

            AirlinesId = airlinesId;
        }

        /// <summary>
        ///     新增审批历史（申请明细）
        /// </summary>
        /// <returns></returns>
        public ApprovalHistory AddNewApprovalHistory(Guid id, int seatingCapacity, decimal carryingCapacity,
            int requestDeliverMonth, string note,
            Guid requestId, Guid planAircraftId, Guid importCategoryId, Guid requestDeliverAnnualId, Guid airlinesId)
        {
            var approvalHistory = new ApprovalHistory();
            approvalHistory.SetSeatingCapacity(seatingCapacity);
            approvalHistory.SetCarryingCapacity(carryingCapacity);
            approvalHistory.SetRequest(requestId);
            approvalHistory.SetNote(note);
            approvalHistory.SetDeliverDate(requestDeliverAnnualId, requestDeliverMonth);
            approvalHistory.SetRequest(requestId);
            approvalHistory.SetPlanAircraft(planAircraftId);
            approvalHistory.SetImportCategory(importCategoryId);
            approvalHistory.SetAirlines(airlinesId);
            approvalHistory.SetId(id);
            ApprovalHistories.Add(approvalHistory);
            return approvalHistory;
        }

        /// <summary>
        ///     设置申请是否已完成申请
        /// </summary>
        public void SetIsFinished(bool isFinished)
        {
            IsFinished = isFinished;
        }

        /// <summary>
        ///     设置提交日期
        /// </summary>
        /// <param name="submitDate">申请提交日期</param>
        public void SetSubmitDate(DateTime? submitDate)
        {
            SubmitDate = submitDate;
        }

        #endregion
    }
}

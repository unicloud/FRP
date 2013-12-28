#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:09:57
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
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Enums;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg
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
        ///     地方局申请文号
        /// </summary>
        public string RaDocNumber { get; private set; }

        /// <summary>
        ///     监管局申请文号
        /// </summary>
        public string SawsDocNumber { get; private set; }

        /// <summary>
        ///     民航局申请文号
        /// </summary>
        public string CaacDocNumber { get; private set; }

        /// <summary>
        ///     民航局申请状态
        /// </summary>
        public RequestStatus Status { get; private set; }

        /// <summary>
        /// 民航局审批意见
        /// </summary>
        public string CaacNote { get; private set; }

        /// <summary>
        /// 地方局审批意见
        /// </summary>
        public string RaNote { get; private set; }

        /// <summary>
        /// 监管局审批意见
        /// </summary>
        public string SawsNote { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///    民航局批文文档外键
        /// </summary>
        public Guid? ApprovalDocId { get; private set; }

        /// <summary>
        ///     地方局申请文档Id
        /// </summary>
        public Guid? RaDocumentId { get; private set; }

        /// <summary>
        ///     监管局申请文档Id
        /// </summary>
        public Guid? SawsDocumentId { get; private set; }

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
        /// 航空公司
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
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("申请标题参数为空！");
            }

            Title = title;
        }

        /// <summary>
        ///     设置地方局申请文号
        /// </summary>
        /// <param name="raDocNumber">地方局申请文号</param>
        public void SetRaDocNumber(string raDocNumber)
        {
            if (string.IsNullOrWhiteSpace(raDocNumber))
            {
                throw new ArgumentException("地方局申请文号参数为空！");
            }

            RaDocNumber = raDocNumber;
        }

        /// <summary>
        ///     设置监管局申请文号
        /// </summary>
        /// <param name="sawsDocNumber">监管局申请文号</param>
        public void SetSawsDocNumber(string sawsDocNumber)
        {
            if (string.IsNullOrWhiteSpace(sawsDocNumber))
            {
                throw new ArgumentException("监管局申请文号参数为空！");
            }

            SawsDocNumber = sawsDocNumber;
        }

        /// <summary>
        ///     设置民航局申请文号
        /// </summary>
        /// <param name="caacDocNumber">民航局申请文号</param>
        public void SetCaacDocNumber(string caacDocNumber)
        {
            if (string.IsNullOrWhiteSpace(caacDocNumber))
            {
                throw new ArgumentException("民航局申请文号参数为空！");
            }

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
        /// 设置民航局审批意见
        /// </summary>
        /// <param name="caacNote"></param>
        public void SetCaacNote(string caacNote)
        {
            if (string.IsNullOrWhiteSpace(caacNote))
            {
                throw new ArgumentException("民航局审批意见参数为空！");
            }

            CaacNote = caacNote;
        }

        /// <summary>
        /// 设置地方局审批意见
        /// </summary>
        /// <param name="raNote"></param>
        public void SetRaNote(string raNote)
        {
            if (string.IsNullOrWhiteSpace(raNote))
            {
                throw new ArgumentException("地方局审批意见参数为空！");
            }

            RaNote = raNote;
        }

        /// <summary>
        /// 设置监管局审批意见
        /// </summary>
        /// <param name="sawsNote"></param>
        public void SetSawsNote(string sawsNote)
        {
            if (string.IsNullOrWhiteSpace(sawsNote))
            {
                throw new ArgumentException("监管局审批意见参数为空！");
            }

            SawsNote = sawsNote;
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
        ///     设置地方局申请文档
        /// </summary>
        /// <param name="raDocumentId">地方局申请文档</param>
        public void SetRaDocument(Guid? raDocumentId)
        {
            RaDocumentId = raDocumentId;
        }

        /// <summary>
        ///     设置监管局申请文档
        /// </summary>
        /// <param name="sawsDocumentId">监管局申请文档</param>
        public void SetSawsDocument(Guid? sawsDocumentId)
        {
            SawsDocumentId = sawsDocumentId;
        }

        /// <summary>
        ///     设置民航局申请文档
        /// </summary>
        /// <param name="caacDocumentId">民航局申请文档</param>
        public void SetCaacDocument(Guid? caacDocumentId)
        {
            CaacDocumentId = caacDocumentId;
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
        /// 新增审批历史（申请明细）
        /// </summary>
        /// <returns></returns>
        public ApprovalHistory AddNewApprovalHistory()
        {
            var approvalHistory = new ApprovalHistory
            {
                RequestId = Id,
                IsApproved = (Status==RequestStatus.已审批),
            };

            approvalHistory.GenerateNewIdentity();
            ApprovalHistories.Add(approvalHistory);

            return approvalHistory;
        }

        #endregion
    }
}

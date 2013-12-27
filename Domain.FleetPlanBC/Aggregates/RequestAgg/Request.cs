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
        public bool IsFinished { get; internal set; }

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
        public Guid? ApprovalDocID { get; private set; }

        /// <summary>
        ///     地方局申请文档Id
        /// </summary>
        public Guid? RaDocumentID { get; private set; }

        /// <summary>
        ///     监管局申请文档Id
        /// </summary>
        public Guid? SawsDocumentID { get; private set; }

        /// <summary>
        ///     民航局申请文档Id
        /// </summary>
        public Guid? CaacDocumentID { get; private set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesID { get; private set; }


        #endregion

        #region 导航属性

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



        #endregion
    }
}

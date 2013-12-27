#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:16:43
// 文件名：ApprovalDoc
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.FleetPlanBC.Enums;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg
{
    /// <summary>
    ///     批文聚合根
    /// </summary>
    public class ApprovalDoc : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ApprovalDoc()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     审批日期
        /// </summary>
        public DateTime? ExamineDate { get; internal set; }

        /// <summary>
        ///     民航局批文文号
        /// </summary>
        public string CaacApprovalNumber { get; private set; }

        /// <summary>
        ///     发改委批文文号
        /// </summary>
        public string NdrcApprovalNumber { get; private set; }

        /// <summary>
        ///     处理状态
        /// </summary>
        public OperationStatus Status { get; private set; }

        /// <summary>
        ///     评审意见
        /// </summary>
        public string Note { get; private set; }

        #endregion

        #region 外键属性
        /// <summary>
        ///     批准单位
        /// </summary>
        public Guid DispatchUnitID { get; private set; }

        /// <summary>
        ///     文档
        /// </summary>
        public Guid? CaacDocumentID { get; private set; }

        /// <summary>
        ///     文档
        /// </summary>
        public Guid? NdrcDocumentID { get; private set; }


        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}

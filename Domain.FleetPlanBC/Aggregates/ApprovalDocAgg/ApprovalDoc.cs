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
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
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
        ///     民航局审批日期
        /// </summary>
        public DateTime? CaacExamineDate { get; private set; }

        /// <summary>
        ///     发改委审批日期
        /// </summary>
        public DateTime? NdrcExamineDate { get; private set; }

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

        /// <summary>
        ///     民航局批文文档名称
        /// </summary>
        public string CaacDocumentName { get; private set; }

        /// <summary>
        ///     发改委批文文档
        /// </summary>
        public string NdrcDocumentName { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     审批单位
        /// </summary>
        public Guid? DispatchUnitId { get; private set; }

        /// <summary>
        ///     民航局批文文档
        /// </summary>
        public Guid? CaacDocumentId { get; private set; }

        /// <summary>
        ///     发改委批文文档
        /// </summary>
        public Guid? NdrcDocumentId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     审批单位
        /// </summary>
        public virtual Manager DispatchUnit { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置处理状态
        /// </summary>
        /// <param name="status">处理状态</param>
        public void SetOperationStatus(OperationStatus status)
        {
            switch (status)
            {
                case OperationStatus.草稿:
                    Status = OperationStatus.草稿;
                    break;
                case OperationStatus.待审核:
                    Status = OperationStatus.待审核;
                    break;
                case OperationStatus.已审核:
                    Status = OperationStatus.已审核;
                    break;
                case OperationStatus.已提交:
                    Status = OperationStatus.已提交;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        /// <summary>
        ///     设置民航局审批日期
        /// </summary>
        /// <param name="date">审批日期</param>
        public void SetCaacExamineDate(DateTime? date)
        {
            CaacExamineDate = date;
        }

        /// <summary>
        /// 设置发改委审批日期
        /// </summary>
        /// <param name="date">审批日期</param>
        public void SetNdrcExamineDate(DateTime? date)
        {
            NdrcExamineDate = date;
        }

        /// <summary>
        ///     设置备注
        /// </summary>
        /// <param name="note">备注</param>
        public void SetNote(string note)
        {
            Note = note;
        }

        /// <summary>
        ///     设置民航局批文文档
        /// </summary>
        /// <param name="caacDocumentId"></param>
        /// <param name="caacDocumentName"></param>
        public void SetCaacDocument(Guid? caacDocumentId, string caacDocumentName)
        {
            CaacDocumentId = caacDocumentId;
            CaacDocumentName = caacDocumentName;
        }

        /// <summary>
        ///     设置民航局批文文号
        /// </summary>
        /// <param name="caacApprovalNumber"></param>
        public void SetCaacApprovalNumber(string caacApprovalNumber)
        {
            CaacApprovalNumber = caacApprovalNumber;
        }

        /// <summary>
        ///     设置发改委批文文档
        /// </summary>
        /// <param name="ndrcDocumentId"></param>
        /// <param name="ndrcDocumentName"></param>
        public void SetNdrcDocument(Guid? ndrcDocumentId, string ndrcDocumentName)
        {
            NdrcDocumentId = ndrcDocumentId;
            NdrcDocumentName = ndrcDocumentName;
        }

        /// <summary>
        ///     设置民航局批文文号
        /// </summary>
        /// <param name="ndrcApprovalNumber"></param>
        public void SetNdrcApprovalNumber(string ndrcApprovalNumber)
        {
            NdrcApprovalNumber = ndrcApprovalNumber;
        }

        /// <summary>
        ///     设置审批单位
        /// </summary>
        /// <param name="dispatchUnitId">审批单位</param>
        public void SetDispatchUnit(Guid dispatchUnitId)
        {
            if (dispatchUnitId == null)
            {
                throw new ArgumentException("审批单位Id为空！");
            }
            DispatchUnitId = dispatchUnitId;
        }

        #endregion
    }
}
#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:00:44
// 文件名：OwnershipHistory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     所有权历史
    /// </summary>
    public class OwnershipHistory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal OwnershipHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        ///     处理状态
        /// </summary>
        public OperationStatus Status { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机外键
        /// </summary>
        public Guid AircraftId { get; internal set; }

        /// <summary>
        ///     所有权人
        /// </summary>
        public int SupplierId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     所有权人
        /// </summary>
        public virtual Supplier Supplier { get; private set; }

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
        ///     设置开始日期
        /// </summary>
        /// <param name="date">开始日期</param>
        public void SetStartDate(DateTime date)
        {
            StartDate = date;
        }

        /// <summary>
        ///     设置结束日期
        /// </summary>
        /// <param name="date">结束日期</param>
        public void SetEndDate(DateTime? date)
        {
            EndDate = date;
        }

        /// <summary>
        ///     设置所有权人
        /// </summary>
        /// <param name="supplier">所有权人</param>
        public void SetSupplier(Supplier supplier)
        {
            if (supplier == null || supplier.IsTransient())
            {
                throw new ArgumentException("所有权人参数为空！");
            }

            Supplier = supplier;
            SupplierId = supplier.Id;
        }

        #endregion
    }
}
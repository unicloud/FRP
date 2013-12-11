#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，12:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Enums;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg
{
    /// <summary>
    ///     接收聚合根
    /// </summary>
    public abstract class Reception : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<ReceptionLine> _lines;
        private HashSet<ReceptionSchedule> _schedules;
        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Reception()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     接收编号
        /// </summary>
        public string ReceptionNumber { get; private set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     是否关闭
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        ///     关闭日期
        /// </summary>
        public DateTime CloseDate { get; private set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///     接收状态
        /// </summary>
        public ReceptionStatus Status { get; private set; }

        /// <summary>
        ///     源Id
        /// </summary>
        public Guid SourceId { get; set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     供应商
        /// </summary>
        public virtual Supplier Supplier { get; private set; }

        /// <summary>
        ///     接收行
        /// </summary>
        public virtual ICollection<ReceptionLine> ReceptionLines
        {
            get { return _lines ?? (_lines = new HashSet<ReceptionLine>()); }
            set { _lines = new HashSet<ReceptionLine>(value); }
        }

        /// <summary>
        ///     交付日程
        /// </summary>
        public virtual ICollection<ReceptionSchedule> ReceptionSchedules
        {
            get { return _schedules ?? (_schedules = new HashSet<ReceptionSchedule>()); }
            set { _schedules = new HashSet<ReceptionSchedule>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置接收状态
        /// </summary>
        /// <param name="status">接收状态</param>
        public void SetStatus(ReceptionStatus status)
        {
            switch (status)
            {
                case ReceptionStatus.开始:
                    Status = ReceptionStatus.开始;
                    break;
                case ReceptionStatus.进行中:
                    Status = ReceptionStatus.进行中;
                    break;
                case ReceptionStatus.撤销:
                    Status = ReceptionStatus.撤销;
                    IsClosed = true;
                    CloseDate = DateTime.Now;
                    break;
                case ReceptionStatus.完成:
                    Status = ReceptionStatus.完成;
                    IsClosed = true;
                    CloseDate = DateTime.Now;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        /// <summary>
        ///     设置供应商
        /// </summary>
        /// <param name="supplier">供应商</param>
        public void SetSupplier(Supplier supplier)
        {
            if (supplier == null || supplier.IsTransient())
            {
                throw new ArgumentException("供应商参数为空！");
            }

            Supplier = supplier;
            SupplierId = supplier.Id;
        }

        /// <summary>
        ///     设置接收编号
        /// </summary>
        /// <param name="seq">流水号</param>
        public void SetReceptionNumber(int seq)
        {
            if (seq < 1)
            {
                throw new ArgumentException("流水号参数为空！");
            }

            var date = DateTime.Now;
            ReceptionNumber = string.Format("{0:yyyyMMdd}{1}", date, seq.ToString("D2"));
        }

        /// <summary>
        ///     设置接收编号
        /// </summary>
        /// <param name="receptionNumber">接收编号</param>
        public void SetReceptionNumber(string receptionNumber)
        {
            if (string.IsNullOrWhiteSpace(receptionNumber))
            {
                throw new ArgumentException("接收编号参数为空！");
            }

            ReceptionNumber = receptionNumber;
        }

        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}
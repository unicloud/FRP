#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
// 方案：FRP
// 项目：Domain.UberModel
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
using UniCloud.Domain.UberModel.Aggregates.OrderAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;
using UniCloud.Domain.UberModel.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.TradeAgg
{
    /// <summary>
    ///     交易聚合根
    /// </summary>
    public class Trade : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<Order> _orders;

        #endregion

        #region 属性

        /// <summary>
        ///     交易编号
        /// </summary>
        public string TradeNumber { get; private set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

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
        public DateTime? CloseDate { get; private set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        ///     交易状态
        /// </summary>
        public TradeStatus Status { get; private set; }

        /// <summary>
        ///     签约对象
        /// </summary>
        public string Signatory { get; private set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

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
        ///     订单集
        /// </summary>
        public virtual ICollection<Order> Orders
        {
            get { return _orders ?? (_orders = new HashSet<Order>()); }
            set { _orders = new HashSet<Order>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置交易状态
        /// </summary>
        /// <param name="status">交易状态</param>
        public void SetStatus(TradeStatus status)
        {
            switch (status)
            {
                case TradeStatus.Start:
                    Status = TradeStatus.Start;
                    break;
                case TradeStatus.InProgress:
                    Status = TradeStatus.InProgress;
                    break;
                case TradeStatus.Repeal:
                    Status = TradeStatus.Repeal;
                    IsClosed = true;
                    CloseDate = DateTime.Now;
                    break;
                case TradeStatus.Complete:
                    Status = TradeStatus.Complete;
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
            Signatory = supplier.Name;
        }

        /// <summary>
        ///     设置交易编号
        /// </summary>
        /// <param name="seq">流水号</param>
        public void SetTradeNumber(int seq)
        {
            if (seq < 1)
            {
                throw new ArgumentException("流水号参数为空！");
            }

            var date = DateTime.Now;
            TradeNumber = string.Format("{0}{1}{2}{3}", date.Year, date.Month, date.Day, seq.ToString("D2"));
        }

        /// <summary>
        ///     设置交易编号
        /// </summary>
        /// <param name="tradeNumber">交易编号</param>
        public void SetTradeNumber(string tradeNumber)
        {
            if (string.IsNullOrWhiteSpace(tradeNumber))
            {
                throw new ArgumentException("交易编号参数为空！");
            }

            TradeNumber = tradeNumber;
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
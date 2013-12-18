#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/18，14:12
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PaymentBC.Enums;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.TradeAgg
{
    /// <summary>
    ///     交易聚合根
    /// </summary>
    public class Trade : EntityInt
    {
        #region 私有字段

        private HashSet<Order> _orders;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Trade()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     交易编号
        /// </summary>
        public string TradeNumber { get; protected set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; protected set; }

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime StartDate { get; protected set; }

        /// <summary>
        ///     是否关闭
        /// </summary>
        public bool IsClosed { get; protected set; }

        /// <summary>
        ///     关闭日期
        /// </summary>
        public DateTime? CloseDate { get; protected set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime? EndDate { get; protected set; }

        /// <summary>
        ///     交易状态
        /// </summary>
        public TradeStatus Status { get; protected set; }

        /// <summary>
        ///     签约对象
        /// </summary>
        public string Signatory { get; protected set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     供应商
        /// </summary>
        public virtual Supplier Supplier { get; protected set; }

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

        #endregion
    }
}
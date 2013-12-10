#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/07，16:12
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
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PaymentBC.Enums;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    /// </summary>
    public class Order : EntityInt
    {
        #region 私有字段

        private HashSet<OrderLine> _lines;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Order()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     版本号
        /// </summary>
        public int Version { get; protected set; }

        /// <summary>
        ///     合同编号
        /// </summary>
        public string ContractNumber { get; protected set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     经办人
        /// </summary>
        public string OperatorName { get; protected set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; protected set; }

        /// <summary>
        ///     生效日期
        /// </summary>
        public DateTime OrderDate { get; protected set; }

        /// <summary>
        ///     撤销日期
        /// </summary>
        public DateTime? RepealDate { get; protected set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; protected set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get; protected set; }

        /// <summary>
        ///     订单状态
        /// </summary>
        public OrderStatus Status { get; protected set; }

        /// <summary>
        ///     合同文档检索ID
        /// </summary>
        public Guid ContractDocGuid { get; protected set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; protected set; }

        /// <summary>
        ///     联系人ID
        /// </summary>
        public int? LinkmanId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     币种
        /// </summary>
        public virtual Currency Currency { get; protected set; }

        /// <summary>
        ///     联系人
        /// </summary>
        public virtual Linkman Linkman { get; protected set; }

        /// <summary>
        ///     订单行
        /// </summary>
        public virtual ICollection<OrderLine> OrderLines
        {
            get { return _lines ?? (_lines = new HashSet<OrderLine>()); }
            set { _lines = new HashSet<OrderLine>(value); }
        }

        #endregion

        #region 操作

        #endregion
    }
}
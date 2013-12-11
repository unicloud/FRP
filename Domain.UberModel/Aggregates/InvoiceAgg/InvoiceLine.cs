#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/09，22:12
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

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.InvoiceAgg
{
    /// <summary>
    ///     发票聚合根
    ///     发票行
    /// </summary>
    public abstract class InvoiceLine : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal InvoiceLine()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     项名称
        /// </summary>
        public string ItemName { get; internal set; }

        /// <summary>
        ///     金额
        /// </summary>
        public decimal Amount { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     发票ID
        /// </summary>
        public int InvoiceId { get; internal set; }

        /// <summary>
        ///     订单行ID
        /// </summary>
        public int OrderLineId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     订单行
        /// </summary>
        public virtual OrderLine OrderLine { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置订单行
        /// </summary>
        /// <param name="orderLine">订单行</param>
        public void SetOrderLine(OrderLine orderLine)
        {
            if (orderLine == null || orderLine.IsTransient())
            {
                throw new ArgumentException("订单行参数为空！");
            }

            OrderLine = orderLine;
            OrderLineId = orderLine.Id;
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
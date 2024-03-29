﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/07，10:58
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.InvoiceAgg
{
    /// <summary>
    ///     发票聚合根
    ///     发票行
    /// </summary>
    public class InvoiceLine : EntityInt, IValidatableObject
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
        public ItemNameType ItemName { get; private set; }

        /// <summary>
        ///     金额/数量
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; private set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作
        /// <summary>
        /// 设置项名称
        /// </summary>
        /// <param name="itemName">项名称</param>
        public void SetItemName(ItemNameType itemName)
        {
            ItemName = itemName;
        }

        /// <summary>
        /// 设置数量/金额
        /// </summary>
        /// <param name="amount">数量/金额</param>
        public void SetAmount(decimal amount)
        {
            Amount = amount;
        }

        /// <summary>
        ///     设置发票行说明
        /// </summary>
        /// <param name="note">说明</param>
        public void SetNote(string note)
        {
            Note = note;
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
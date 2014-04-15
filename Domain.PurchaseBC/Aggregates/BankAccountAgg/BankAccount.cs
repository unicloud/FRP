#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.BankAccountAgg
{
    /// <summary>
    ///     银行账户聚合根
    /// </summary>
    public class BankAccount : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal BankAccount()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        ///     开户人
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     开户行
        /// </summary>
        public string Bank { get; set; }

        /// <summary>
        ///     开户行分支
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        ///     国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     开户地址（中文）
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     是否当前默认账号
        /// </summary>
        public bool IsCurrent { get; set; }

        public string CustCode { get; set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

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
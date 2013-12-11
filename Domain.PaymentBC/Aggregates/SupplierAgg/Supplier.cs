#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/07，17:12
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using UniCloud.Domain.PaymentBC.Aggregates.BankAccountAgg;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg
{
    /// <summary>
    ///     供应商聚合根
    /// </summary>
    public class Supplier : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Supplier()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     组织机构代码
        /// </summary>
        public string Code { get; protected set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; protected set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; protected set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        ///     银行账户
        /// </summary>
        public ICollection<BankAccount> BankAccounts { get; protected set; }

        #endregion

        #region 操作

        #endregion
    }
}
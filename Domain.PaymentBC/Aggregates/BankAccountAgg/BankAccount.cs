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

namespace UniCloud.Domain.PaymentBC.Aggregates.BankAccountAgg
{
    /// <summary>
    ///     银行账户聚合根
    /// </summary>
    public class BankAccount : EntityInt
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
        public string Account { get; protected set; }

        /// <summary>
        ///     开户人
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     开户行
        /// </summary>
        public string Bank { get; protected set; }

        /// <summary>
        ///     开户行分支
        /// </summary>
        public string Branch { get; protected set; }

        /// <summary>
        ///     国家
        /// </summary>
        public string Country { get; protected set; }

        /// <summary>
        ///     开户地址（中文）
        /// </summary>
        public string Address { get; protected set; }

        /// <summary>
        ///     是否当前默认账号
        /// </summary>
        public bool IsCurrent { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get; protected set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}
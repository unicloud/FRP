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

namespace UniCloud.Domain.PaymentBC.Aggregates.LinkmanAgg
{
    /// <summary>
    ///     联系人聚合根
    /// </summary>
    public class Linkman : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Linkman()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     姓名
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        ///     是否默认联系人
        /// </summary>
        public bool IsDefault { get; protected set; }

        /// <summary>
        ///     电话
        /// </summary>
        public string TelePhone { get; protected set; }

        /// <summary>
        ///     手机
        /// </summary>
        public string Mobile { get; protected set; }

        /// <summary>
        ///     传真
        /// </summary>
        public string Fax { get; protected set; }

        /// <summary>
        ///     邮件账号
        /// </summary>
        public string Email { get; protected set; }

        /// <summary>
        ///     公司部门
        /// </summary>
        public string Department { get; protected set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; protected set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}
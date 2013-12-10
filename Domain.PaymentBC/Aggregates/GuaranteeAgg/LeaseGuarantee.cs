#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，10:58
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.GuaranteeAgg
{
    /// <summary>
    ///     保函聚合根
    ///     租赁保函
    /// </summary>
    public class LeaseGuarantee : Guarantee
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal LeaseGuarantee()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     订单ID
        /// </summary>
        public int OrderId { get; private set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置订单ID
        /// </summary>
        /// <param name="id">订单ID</param>
        public void SetOrderId(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("订单ID参数为空！");
            }

            OrderId = id;
        }

        #endregion
    }
}
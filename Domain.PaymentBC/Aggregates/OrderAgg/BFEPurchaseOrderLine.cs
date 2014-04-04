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

using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    ///     发动机购买订单行
    /// </summary>
    public class BFEPurchaseOrderLine : OrderLine
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal BFEPurchaseOrderLine()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     交易状态
        /// </summary>
        public BFEStatus Status { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     BFE物料ID
        /// </summary>
        public int BFEMaterialId { get; protected set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}
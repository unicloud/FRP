﻿#region 版本信息

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

namespace UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg
{
    /// <summary>
    ///     币种聚合根
    /// </summary>
    public class Currency : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Currency()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     币种中文名称
        /// </summary>
        public string CnName { get; protected set; }

        /// <summary>
        ///     币种英文名称
        /// </summary>
        public string EnName { get; protected set; }

        /// <summary>
        ///     货币符号
        /// </summary>
        public string Symbol { get; protected set; }

        /// <summary>
        ///     对RMB的汇率
        ///     <remarks>
        ///         记录当前值
        ///     </remarks>
        /// </summary>
        public decimal ExchangeRate { get; protected set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}
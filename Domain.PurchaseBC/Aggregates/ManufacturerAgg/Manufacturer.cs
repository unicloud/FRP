﻿#region 版本信息

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

namespace UniCloud.Domain.PurchaseBC.Aggregates.ManufacturerAgg
{
    /// <summary>
    ///     制造商聚合根
    /// </summary>
    public class Manufacturer : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Manufacturer()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     中文名称
        /// </summary>
        public string CnName { get; internal set; }

        /// <summary>
        ///     公司英文名称
        /// </summary>
        public string EnName { get; internal set; }

        /// <summary>
        ///     公司中文简称
        /// </summary>
        public string CnShortName { get; internal set; }

        /// <summary>
        ///     公司英文简称
        /// </summary>
        public string EnShortName { get; internal set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; internal set; }

        /// <summary>
        ///     制造商类型，1--表示飞机制造商，2--表示发动机制造商
        /// </summary>
        public int Type { get; internal set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}
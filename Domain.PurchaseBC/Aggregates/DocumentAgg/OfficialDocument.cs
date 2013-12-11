#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/09，23:12
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.DocumentAgg
{
    /// <summary>
    ///     文档聚合根
    ///     公文
    /// </summary>
    public class OfficialDocument : Document
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal OfficialDocument()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     文号
        /// </summary>
        public string ReferenceNumber { get; protected set; }

        /// <summary>
        ///     发文单位
        /// </summary>
        public string DispatchUnit { get; protected set; }

        /// <summary>
        ///     发文日期
        /// </summary>
        public DateTime DispatchDate { get; protected set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}
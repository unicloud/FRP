#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/06/28，17:06
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.AnnualAgg
{
    /// <summary>
    ///     年度聚合根
    /// </summary>
    public class Annual : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Annual()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     年度
        /// </summary>
        public int Year { get; internal set; }

        /// <summary>
        ///     是否打开年度
        /// </summary>
        public bool IsOpen { get; internal set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}
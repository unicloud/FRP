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

namespace UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg
{
    /// <summary>
    ///     活动类型聚合根
    /// </summary>
    public class ActionCategory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ActionCategory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     活动类型
        /// </summary>
        public string ActionType { get; protected set; }

        /// <summary>
        ///     活动类型名称
        /// </summary>
        public string ActionName { get; protected set; }

        /// <summary>
        ///     需要审批
        /// </summary>
        public bool NeedRequest { get; protected set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion
    }
}
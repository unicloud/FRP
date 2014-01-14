#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/14，17:01
// 文件名：PlanHistoryStyleRule.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Windows;
using Telerik.Windows.Controls;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryPlans
{
    /// <summary>
    /// 计划历史比较样式
    /// </summary>
    public class PlanHistoryStyleRule : StyleSelector
    {
        /// <summary>
        ///     增加的样式
        /// </summary>
        public Style AddedStyle { get; set; }

        /// <summary>
        ///     删除的样式
        /// </summary>
        public Style RemovedStyle { get; set; }

        /// <summary>
        ///     修改的样式
        /// </summary>
        public Style ModifiedStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is PlanHistoryDTO)
            {
                var planHistory = item as PlanHistoryDTO;
            }
            return null;
        }
    }
}
#region 命名空间

using System.Windows;
using Telerik.Windows.Controls;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryPlans
{
    /// <summary>
    ///     计划历史比较样式
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
                if (planHistory.PlanHistoryCompareStatus == PlanHistoryCompareStatus.Added)
                {
                    return AddedStyle;
                }
                if (planHistory.PlanHistoryCompareStatus == PlanHistoryCompareStatus.Modified)
                {
                    return ModifiedStyle;
                }
                if (planHistory.PlanHistoryCompareStatus == PlanHistoryCompareStatus.Removed)
                {
                    return RemovedStyle;
                }
            }
            return null;
        }
    }
}
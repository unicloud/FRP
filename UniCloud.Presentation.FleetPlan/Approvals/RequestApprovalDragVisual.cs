﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/30，11:12
// 文件名：RequestPlanDragVisual.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using System.Windows;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;

#endregion

namespace UniCloud.Presentation.FleetPlan.Approvals
{
    /// <summary>
    ///     拖放展示
    /// </summary>
    public class RequestApprovalDragVisual : IDragVisualProvider
    {
        public bool UseDefaultCursors { get; set; }

        public FrameworkElement CreateDragVisual(DragVisualProviderState state)
        {
            var visual = new DragVisual
            {
                Content = state.DraggedItems.OfType<object>().FirstOrDefault(),
                ContentTemplate = state.Host.Resources["EnRouteDraggedItemTemplate"] as DataTemplate
            };
            return visual;
        }

        public Point GetDragVisualOffset(DragVisualProviderState state)
        {
            return state.RelativeStartPoint;
        }
    }
}
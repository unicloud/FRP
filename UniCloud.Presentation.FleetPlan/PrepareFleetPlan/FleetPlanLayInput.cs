#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/7 14:41:55
// 文件名：FleetPlanLayInput
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.DragDrop.Behaviors;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    #region 计划飞机

    /// <summary>
    /// 拖放行为
    /// </summary>
    public class FleetPlanLayPlanDragDrop : GridViewDragDropBehavior
    {
        public override bool CanStartDrag(GridViewDragDropState state)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
            // 当前计划不为空且还未审核通过的，才允许开始拖放。
            return viewModel.CurPlan != null && viewModel.CurPlan.Status < (int)PlanStatus.已审核;
        }

        public override bool CanDrop(GridViewDragDropState state)
        {
            return false;
        }

        public override void DragDropCompleted(GridViewDragDropState state)
        {
        }
    }

    /// <summary>
    /// 拖放展示
    /// </summary>
    public class FleetPlanLayPlanDragVisual : IDragVisualProvider
    {
        public FrameworkElement CreateDragVisual(DragVisualProviderState state)
        {
            var visual = new Telerik.Windows.DragDrop.DragVisual
            {
                Content = state.DraggedItems.OfType<object>().FirstOrDefault(),
                ContentTemplate = state.Host.Resources["PlanDraggedItemTemplate"] as DataTemplate
            };
            return visual;
        }

        public Point GetDragVisualOffset(DragVisualProviderState state)
        {
            return state.RelativeStartPoint;
        }

        public bool UseDefaultCursors { get; set; }
    }

    /// <summary>
    /// 鼠标双击逻辑
    /// </summary>
    public class FleetPlanLayPlanDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
            var planAircraft = cell.DataContext as PlanAircraftDTO;
            viewModel.OpenEditDialog(planAircraft, PlanDetailCreateSource.PlanAircraft);
        }

        protected override bool CanDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
            // 当前计划不为空且还未审核通过的，才允许双击。
            return viewModel.CurPlan != null && viewModel.CurPlan.Status < (int)PlanStatus.已审核;
        }
    }

    #endregion

    #region 运营中的飞机

    public class FleetPlanLayOperationDragDrop : GridViewDragDropBehavior
    {
        public override bool CanStartDrag(GridViewDragDropState state)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
            // 当前计划不为空且还未审核通过的，才允许开始拖放。
            return viewModel.CurPlan != null && viewModel.CurPlan.Status < (int)PlanStatus.已审核;
        }

        public override bool CanDrop(GridViewDragDropState state)
        {
            return false;
        }

        public override void DragDropCompleted(GridViewDragDropState state)
        {
        }
    }

    /// <summary>
    /// 拖放展示
    /// </summary>
    public class FleetPlanLayOperationDragVisual : IDragVisualProvider
    {
        public FrameworkElement CreateDragVisual(DragVisualProviderState state)
        {
            var visual = new Telerik.Windows.DragDrop.DragVisual
            {
                Content = state.DraggedItems.OfType<object>().FirstOrDefault(),
                ContentTemplate = state.Host.Resources["OperationDraggedItemTemplate"] as DataTemplate
            };
            return visual;
        }

        public Point GetDragVisualOffset(DragVisualProviderState state)
        {
            return state.RelativeStartPoint;
        }

        public bool UseDefaultCursors { get; set; }
    }

    /// <summary>
    /// 鼠标双击逻辑
    /// </summary>
    public class FleetPlanLayOperationDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
            var aircraft = cell.DataContext as AircraftDTO;
            if (aircraft != null)
            {
                var planAircraft = viewModel.PlanAircrafts.FirstOrDefault(p => p.Id == aircraft.AircraftId);
                viewModel.OpenEditDialog(planAircraft, PlanDetailCreateSource.Aircraft);
            }
        }

        protected override bool CanDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
            // 当前计划不为空且还未审核通过的，才允许双击。
            return viewModel.CurPlan != null && viewModel.CurPlan.Status < (int)PlanStatus.已审核;
        }
    }

    #endregion

    #region 当前计划明细

    public class FleetPlanLayCurrentPlanDetail : GridViewDragDropBehavior
    {
        public override bool CanStartDrag(GridViewDragDropState state)
        {
            return false;
        }

        public override bool CanDrop(GridViewDragDropState state)
        {
            return true;
        }

        public override void Drop(GridViewDragDropState state)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
            var items = (from object item in state.DraggedItems select item).ToList();
            if (items.Any())
            {
                if (items[0] is PlanAircraftDTO)
                {
                    var planAircraft = items.Select(pa => pa as PlanAircraftDTO).FirstOrDefault();
                    viewModel.OpenEditDialog(planAircraft, PlanDetailCreateSource.PlanAircraft);
                }
                else if (items[0] is AircraftDTO)
                {
                    var planAircraft = items.SelectMany(a =>
                    {
                        var aircraft = a as AircraftDTO;
                        return aircraft != null ? viewModel.PlanAircrafts.Where(p => p.Id == aircraft.AircraftId) : null;
                    }).FirstOrDefault(pa => pa.IsOwn);
                    viewModel.OpenEditDialog(planAircraft, PlanDetailCreateSource.Aircraft);
                }
            }
        }
    }

    #endregion
}

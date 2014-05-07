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
using Telerik.Windows.Controls;
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
    public class FleetPlanLayPlanDragDrop : GridViewDragAndDropBehavior
    {
        //public override bool CanStartDrag(GridViewDragDropState state)
        //{
        //    var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
        //    // 当前计划不为空且还未审核通过的，才允许开始拖放。
        //    return viewModel.CurPlan != null && viewModel.CurPlan.Status < (int)PlanStatus.已审核;
        //}
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

    public class FleetPlanLayOperationDragDrop : GridViewDragAndDropBehavior
    {
        //public override bool CanStartDrag(GridViewDragDropState state)
        //{
        //    var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
        //    // 当前计划不为空且还未审核通过的，才允许开始拖放。
        //    return viewModel.CurPlan != null && viewModel.CurPlan.Status < (int)PlanStatus.已审核;
        //}
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
                var planAircraft = viewModel.AllPlanAircrafts.SourceCollection.Cast<PlanAircraftDTO>().FirstOrDefault(p => p.AircraftId == aircraft.AircraftId);
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
}

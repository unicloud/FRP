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

using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Controls.GridView;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{

    #region 计划飞机

    /// <summary>
    ///     鼠标双击逻辑
    /// </summary>
    public class FleetPlanLayPlanDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
            var planAircraft = cell.DataContext as PlanAircraftDTO;
            viewModel.OpenEditDialog(planAircraft, PlanDetailCreateSource.PlanAircraft);
        }

        protected override bool CanDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
            // 当前计划不为空且还未审核通过的，才允许双击。
            return viewModel.CurPlan != null && viewModel.CurPlan.Status < (int) PlanStatus.已审核;
        }
    }

    #endregion

    #region 运营中的飞机

    /// <summary>
    ///     鼠标双击逻辑
    /// </summary>
    public class FleetPlanLayOperationDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
            var aircraft = cell.DataContext as AircraftDTO;
            if (aircraft != null)
            {
                var planAircraft =
                    viewModel.AllPlanAircrafts.SourceCollection.Cast<PlanAircraftDTO>()
                        .FirstOrDefault(p => p.AircraftId == aircraft.AircraftId);
                viewModel.OpenEditDialog(planAircraft, PlanDetailCreateSource.Aircraft);
            }
        }

        protected override bool CanDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FleetPlanLayVM>();
            // 当前计划不为空且还未审核通过的，才允许双击。
            return viewModel.CurPlan != null && viewModel.CurPlan.Status < (int) PlanStatus.已审核;
        }
    }

    #endregion
}
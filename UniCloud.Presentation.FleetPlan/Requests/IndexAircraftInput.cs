#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/3 15:14:24
// 文件名：IndexAircraftInput
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Controls.GridView;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{

    #region PlanHistory

    /// <summary>
    ///     计划明细双击处理
    /// </summary>
    public class PlanDetailDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ManageIndexAircraftVM>();
            var planDetail = cell.DataContext as PlanHistoryDTO;
            viewModel.AddNewRequestDetail(planDetail);
        }

        protected override bool CanDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ManageIndexAircraftVM>();
            var planDetail = cell.DataContext as PlanHistoryDTO;
            // 选中申请还未审核通过，且双击的是可申请的计划明细，才允许双击。
            return planDetail != null && viewModel.CurRequest != null &&
                   viewModel.CurRequest.Status < (int) RequestStatus.已审核 &&
                   (planDetail.CanRequest == (int) CanRequest.可申请 || planDetail.CanRequest == (int) CanRequest.可再次申请);
        }
    }

    #endregion

    #region ApprovalHistory

    /// <summary>
    ///     申请明细双击处理
    /// </summary>
    public class ApprovalDetailDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ManageIndexAircraftVM>();
            var requestDetail = cell.DataContext as ApprovalHistoryDTO;
            viewModel.RemoveRequestDetail(requestDetail);
        }

        protected override bool CanDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ManageIndexAircraftVM>();
            // 选中申请还未审核通过，才允许双击。
            return viewModel.CurRequest != null && viewModel.CurRequest.Status < (int) RequestStatus.已审核;
        }
    }

    #endregion
}
#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/9 9:17:45
// 文件名：ApprovalInput
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/9 9:17:45
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

namespace UniCloud.Presentation.FleetPlan.Approvals
{

    #region 在途申请

    /// <summary>
    ///     鼠标双击逻辑
    /// </summary>
    public class EnRouteRequestDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ApprovalVM>();
            var request = cell.DataContext as RequestDTO;
            viewModel.AddRequestToApprovalDoc(request);
        }

        protected override bool CanDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ApprovalVM>();
            var request = cell.DataContext as RequestDTO;
            // 选中批文还未审核通过，且双击的是已提交的申请，才允许双击。
            return request != null && viewModel.SelApprovalDoc != null &&
                   viewModel.SelApprovalDoc.Status < (int) OperationStatus.已审核 &&
                   request.Status == (int) RequestStatus.已提交;
        }
    }

    #endregion

    #region 批文的申请

    /// <summary>
    ///     鼠标双击逻辑
    /// </summary>
    public class ApprovalRequestDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ApprovalVM>();
            var request = cell.DataContext as RequestDTO;
            viewModel.RemoveRequest(request);
        }

        protected override bool CanDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ApprovalVM>();
            // 选中批文还未审核通过，才允许双击。
            return viewModel.SelApprovalDoc != null && viewModel.SelApprovalDoc.Status < (int) OperationStatus.已审核;
        }
    }

    #endregion
}
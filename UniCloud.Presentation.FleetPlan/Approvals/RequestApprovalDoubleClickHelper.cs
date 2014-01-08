#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/07，13:01
// 文件名：RequestApprovalDoubleClickHelper.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion


#region 命名空间

using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Controls.GridView;
using UniCloud.Presentation.FleetPlan.Requests;
using UniCloud.Presentation.Input;

#endregion

namespace UniCloud.Presentation.FleetPlan.Approvals
{
    public class RequestApprovalDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ApprovalVM>();
            viewModel.AddRequest(viewModel.SelectedEnRouteRequest);
        }

        protected override bool CanDoubleClick(GridViewCellBase cell)
        {
            return true;
        }
    }
}
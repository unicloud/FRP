#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/07，13:01
// 文件名：ApprovalDoubleClickHelper.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Controls.GridView;
using UniCloud.Presentation.Input;

namespace UniCloud.Presentation.FleetPlan.Approvals
{
    /// <summary>
    /// 批文双击
    /// </summary>
    public class ApprovalDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(GridViewCellBase cell)
        {
            var viewModel = ServiceLocator.Current.GetInstance<ApprovalVM>();
            viewModel.RemoveRequest(viewModel.SelectedRequest);
        }

        protected override bool CanDoubleClick(GridViewCellBase cell)
        {
            return true;
        }
    }
}

#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/07，13:01
// 文件名：RequestApprovalDragDrop.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion


#region 命名空间

using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.DragDrop.Behaviors;
using UniCloud.Presentation.FleetPlan.Requests;

#endregion

namespace UniCloud.Presentation.FleetPlan.Approvals
{
    public class RequestApprovalDragDrop : GridViewDragDropBehavior
    {
        public override bool CanStartDrag(GridViewDragDropState state)
        {
            //return true;
            var viewModel = ServiceLocator.Current.GetInstance<RequestVM>();
            var items = (from object item in state.DraggedItems select item).ToList();
            if (items.Count<1)
            {
                return false;
            }
            // 选中申请还未审核通过，且拖动的是可申请的计划明细，才允许开始拖放。
            return viewModel.DragPlanHistory(items[0]);
        }

        public override bool CanDrop(GridViewDragDropState state)
        {
            return true;
        }

        public override void Drop(GridViewDragDropState state)
        {
            var viewModel = ServiceLocator.Current.GetInstance<RequestVM>();
        }

        public override void DragDropCompleted(GridViewDragDropState state)
        {
        }
    }
}
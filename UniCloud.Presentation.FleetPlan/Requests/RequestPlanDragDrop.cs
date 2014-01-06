#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/30，11:12
// 文件名：RequestPlanDragDrop.cs
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

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{
    public class RequestPlanDragDrop : GridViewDragDropBehavior
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
            //planDetail != null && viewModel.CurrentRequestDataObject != null &&
            //viewModel.CurrentRequestDataObject.Status < (int)ReqStatus.Checked && planDetail.CanRequest == "1：可申请";
        }

        public override bool CanDrop(GridViewDragDropState state)
        {
            // 拖动的是申请明细时，才可以释放。
            var items = (from object item in state.DraggedItems select item).ToList();
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
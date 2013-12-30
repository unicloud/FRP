#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/30，11:12
// 文件名：RequestDragDrop.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.DragDrop.Behaviors;

namespace UniCloud.Presentation.FleetPlan.Requests
{
 
    /// <summary>
    /// 拖放行为
    /// </summary>
    public class RequestDragDrop : GridViewDragDropBehavior
    {
        public override bool CanStartDrag(GridViewDragDropState state)
        {
            var viewModel = ServiceLocator.Current.GetInstance<RequestVM>();
            // 选中申请还未审核通过，才允许开始拖放。
            return true;
            //  return viewModel.CurrentRequestDataObject != null && viewModel.CurrentRequestDataObject.Status < (int)ReqStatus.Checked;
        }

        public override bool CanDrop(GridViewDragDropState state)
        {
            //拖动的是计划明细时，才可以释放。
            var items = (from object item in state.DraggedItems select item).ToList();
            return true;
        }

        public override void Drop(GridViewDragDropState state)
        {
            var viewModel = ServiceLocator.Current.GetInstance<RequestVM>();
            var items = (from object item in state.DraggedItems select item).ToList();
        }

        public override void DragDropCompleted(GridViewDragDropState state)
        {
        }
    }
}
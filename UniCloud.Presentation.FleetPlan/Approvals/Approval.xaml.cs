#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/30，08:12
// 文件名：Approval.xaml.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;
using DragEventArgs = Telerik.Windows.DragDrop.DragEventArgs;
using DragVisual = Telerik.Windows.DragDrop.DragVisual;

#endregion

namespace UniCloud.Presentation.FleetPlan.Approvals
{
    [Export]
    public partial class Approval
    {
        public Approval()
        {
            InitializeComponent();
            DragDropManager.AddDragInitializeHandler(EnRouteRequests, OnEnRouteDragInitialize);
            DragDropManager.AddDragOverHandler(EnRouteRequests, OnEnRouteDragOver);
            DragDropManager.AddDropHandler(EnRouteRequests, OnEnRouteDrop);

            DragDropManager.AddDragInitializeHandler(ApprovalRequests, OnApprovalDragInitialize);
            DragDropManager.AddDragOverHandler(ApprovalRequests, OnApprovalDragOver);
            DragDropManager.AddDropHandler(ApprovalRequests, OnApprovalDrop);
        }

        [Import(typeof(ApprovalVM))]
        public ApprovalVM ViewModel
        {
            get { return DataContext as ApprovalVM; }
            set { DataContext = value; }
        }

        private void OnEnRouteDragInitialize(object sender, DragInitializeEventArgs e)
        {
            var details = new DropIndicationDetails();
            GridViewRow row = e.OriginalSource as GridViewRow ??
                              (e.OriginalSource as FrameworkElement).ParentOfType<GridViewRow>();

            var gridView = sender as RadGridView;
            if (gridView == null) return;

            object item = row != null ? row.Item : gridView.SelectedItem;
            details.CurrentDraggedItem = item;

            IDragPayload dragPayload = DragDropPayloadManager.GeneratePayload(null);

            dragPayload.SetData("DraggedData", item);
            dragPayload.SetData("DropDetails", details);

            e.Data = dragPayload;

            //判断是否能拖拽
            var request = item as RequestDTO;
            if (request != null && ViewModel.SelApprovalDoc != null &&
                   ViewModel.SelApprovalDoc.Status < (int)OperationStatus.已审核 &&
                   request.Status == (int)RequestStatus.已提交)
            {
                e.AllowedEffects = DragDropEffects.All;
            }
            else
            {
                e.AllowedEffects = DragDropEffects.None;
            }
            e.DragVisual = new DragVisual
            {
                Content = item,
                ContentTemplate = gridView.Resources["EnRouteDraggedItemTemplate"] as DataTemplate
            };
            e.DragVisualOffset = e.RelativeStartPoint;
        }

        private void OnEnRouteDragOver(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            if (draggedItem == null || ApprovalRequests == null) return;

            var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;
            if (dropDetails == null) return;
            dropDetails.CurrentDraggedOverItem = ApprovalRequests;
            dropDetails.CurrentDropPosition = DropIndicationDetails.ConverDropPositionToString(DropPosition.Inside);

            e.Handled = true;
        }

        private void OnEnRouteDrop(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            var details = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if (details == null || draggedItem == null)
            {
                return;
            }

            if (e.Effects != DragDropEffects.None)
            {
                if (draggedItem is RequestDTO)
                {
                    var request = draggedItem as RequestDTO;
                    ViewModel.RemoveRequest(request);
                }
            }

            e.Handled = true;
        }


        private void OnApprovalDragInitialize(object sender, DragInitializeEventArgs e)
        {
            var details = new DropIndicationDetails();
            GridViewRow row = e.OriginalSource as GridViewRow ??
                              (e.OriginalSource as FrameworkElement).ParentOfType<GridViewRow>();

            var gridView = sender as RadGridView;
            if (gridView == null) return;

            object item = row != null ? row.Item : gridView.SelectedItem;
            details.CurrentDraggedItem = item;

            IDragPayload dragPayload = DragDropPayloadManager.GeneratePayload(null);

            dragPayload.SetData("DraggedData", item);
            dragPayload.SetData("DropDetails", details);

            e.Data = dragPayload;

            //判断是否能拖拽
            if (ViewModel.SelApprovalDoc != null && ViewModel.SelApprovalDoc.Status < (int)OperationStatus.已审核)
            {
                e.AllowedEffects = DragDropEffects.All;
            }
            else
            {
                e.AllowedEffects = DragDropEffects.None;
            }

            e.DragVisual = new DragVisual
            {
                Content = item,
                ContentTemplate = gridView.Resources["ApproveDraggedItemTemplate"] as DataTemplate
            };
            e.DragVisualOffset = e.RelativeStartPoint;
        }

        private void OnApprovalDragOver(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            if (draggedItem == null || EnRouteRequests == null) return;

            var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;
            if (dropDetails == null) return;
            dropDetails.CurrentDraggedOverItem = EnRouteRequests;
            dropDetails.CurrentDropPosition = DropIndicationDetails.ConverDropPositionToString(DropPosition.Inside);

            e.Handled = true;
        }


        private void OnApprovalDrop(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            var details = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if (details == null || draggedItem == null)
            {
                return;
            }

            if (e.Effects != DragDropEffects.None)
            {
                if (draggedItem is RequestDTO)
                {
                    var request = draggedItem as RequestDTO;
                    ViewModel.AddRequestToApprovalDoc(request);
                }
            }

            e.Handled = true;
        }

    }
}
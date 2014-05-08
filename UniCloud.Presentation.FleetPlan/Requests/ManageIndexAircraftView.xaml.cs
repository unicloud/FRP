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

namespace UniCloud.Presentation.FleetPlan.Requests
{
    [Export(typeof (ManageIndexAircraftView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageIndexAircraftView
    {
        public ManageIndexAircraftView()
        {
            InitializeComponent();
            DragDropManager.AddDragInitializeHandler(PlanDetail, OnPlanDatailDragInitialize);
            DragDropManager.AddDragOverHandler(PlanDetail, OnPlanDetailDragOver);
            DragDropManager.AddDropHandler(PlanDetail, OnPlanDetailDrop);

            DragDropManager.AddDragInitializeHandler(ApprovalDetail, OnApprovalDetailDragInitialize);
            DragDropManager.AddDragOverHandler(ApprovalDetail, OnApprovalDetailDragOver);
            DragDropManager.AddDropHandler(ApprovalDetail, OnApprovalDetailDrop);
        }

        [Import]
        public ManageIndexAircraftVM ViewModel
        {
            get { return DataContext as ManageIndexAircraftVM; }
            set { DataContext = value; }
        }

        private void OnPlanDatailDragInitialize(object sender, DragInitializeEventArgs e)
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
            var planDetail = item as PlanHistoryDTO;
            if (planDetail != null && ViewModel.CurRequest != null &&
                ViewModel.CurRequest.Status < (int) RequestStatus.已审核 &&
                (planDetail.CanRequest == (int) CanRequest.可申请 || planDetail.CanRequest == (int) CanRequest.可再次申请))
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
                ContentTemplate = gridView.Resources["PlanDetailDraggedItemTemplate"] as DataTemplate
            };
            e.DragVisualOffset = e.RelativeStartPoint;
        }

        private void OnPlanDetailDrop(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            var details = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if (details == null || draggedItem == null)
            {
                return;
            }

            if (e.Effects != DragDropEffects.None)
            {
                if (draggedItem is ApprovalHistoryDTO)
                {
                    var approvalHistory = draggedItem as ApprovalHistoryDTO;
                    ViewModel.RemoveRequestDetail(approvalHistory);
                }
            }

            e.Handled = true;
        }

        private void OnPlanDetailDragOver(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            if (draggedItem == null || ApprovalDetail == null) return;

            var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;
            if (dropDetails == null) return;
            dropDetails.CurrentDraggedOverItem = ApprovalDetail;
            dropDetails.CurrentDropPosition = DropIndicationDetails.ConverDropPositionToString(DropPosition.Inside);

            e.Handled = true;
        }

        private void OnApprovalDetailDragInitialize(object sender, DragInitializeEventArgs e)
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
            if (ViewModel.CurRequest != null && ViewModel.CurRequest.Status < (int) RequestStatus.已审核)
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
                ContentTemplate = gridView.Resources["ApprovalDetailDraggedItemTemplate"] as DataTemplate
            };
            e.DragVisualOffset = e.RelativeStartPoint;
        }

        private void OnApprovalDetailDrop(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            var details = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if (details == null || draggedItem == null)
            {
                return;
            }

            if (e.Effects != DragDropEffects.None)
            {
                if (draggedItem is PlanHistoryDTO)
                {
                    var planHistory = draggedItem as PlanHistoryDTO;
                    ViewModel.AddNewRequestDetail(planHistory);
                }
            }

            e.Handled = true;
        }

        private void OnApprovalDetailDragOver(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            if (draggedItem == null || PlanDetail == null) return;

            var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;
            if (dropDetails == null) return;
            dropDetails.CurrentDraggedOverItem = PlanDetail;
            dropDetails.CurrentDropPosition = DropIndicationDetails.ConverDropPositionToString(DropPosition.Inside);

            e.Handled = true;
        }
    }
}
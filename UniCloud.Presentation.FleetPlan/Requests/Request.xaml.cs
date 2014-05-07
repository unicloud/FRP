#region 命名空间

using System.ComponentModel.Composition;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using DragEventArgs = Telerik.Windows.DragDrop.DragEventArgs;
using DragVisual = Telerik.Windows.DragDrop.DragVisual;

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{
    [Export(typeof (Request))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class Request
    {
        public Request()
        {
            InitializeComponent();
            DragDropManager.AddDragInitializeHandler(PlanDetail, OnPlanDatailDragInitialize);
            DragDropManager.AddDragOverHandler(PlanDetail, OnPlanDetailDragOver);
            DragDropManager.AddDropHandler(PlanDetail, OnPlanDetailDrop);

            DragDropManager.AddDragInitializeHandler(RequestDetail, OnRequestDetailDragInitialize);
            DragDropManager.AddDragOverHandler(RequestDetail, OnRequestDetailDragOver);
            DragDropManager.AddDropHandler(RequestDetail, OnRequestDetailDrop);
        }

        [Import]
        public RequestVM ViewModel
        {
            get { return DataContext as RequestVM; }
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

            e.DragVisual = new DragVisual
            {
                Content = item,
                ContentTemplate = gridView.Resources["PlanDraggedItemTemplate"] as DataTemplate
            };
            e.DragVisualOffset = e.RelativeStartPoint;
            e.AllowedEffects = DragDropEffects.All;
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
                    var planAircraft = draggedItem as ApprovalHistoryDTO;
                }
            }

            e.Handled = true;
        }

        private void OnPlanDetailDragOver(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            if (draggedItem == null || RequestDetail == null) return;

            var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;
            if (dropDetails == null) return;
            dropDetails.CurrentDraggedOverItem = RequestDetail;
            dropDetails.CurrentDropPosition = DropIndicationDetails.ConverDropPositionToString(DropPosition.Inside);

            e.Handled = true;
        }

        private void OnRequestDetailDragInitialize(object sender, DragInitializeEventArgs e)
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

            e.DragVisual = new DragVisual
            {
                Content = item,
                ContentTemplate = gridView.Resources["RequestDraggedItemTemplate"] as DataTemplate
            };
            e.DragVisualOffset = e.RelativeStartPoint;
            e.AllowedEffects = DragDropEffects.All;
        }

        private void OnRequestDetailDrop(object sender, DragEventArgs e)
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
                    var planAircraft = draggedItem as PlanHistoryDTO;
                }
            }

            e.Handled = true;
        }

        private void OnRequestDetailDragOver(object sender, DragEventArgs e)
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
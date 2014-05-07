using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;
using DragEventArgs = Telerik.Windows.DragDrop.DragEventArgs;
using DragVisual = Telerik.Windows.DragDrop.DragVisual;

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class FleetPlanLay
    {
        public FleetPlanLay()
        {
            InitializeComponent();
            DragDropManager.AddDragInitializeHandler(PlanAircraft, OnPlanAircraftDragInitialize);
            DragDropManager.AddDragInitializeHandler(Aircraft, OnAircraftDragInitialize);
            DragDropManager.AddDragOverHandler(PlanLay, OnPlanLayDragOver);
            DragDropManager.AddDropHandler(PlanLay, OnPlanLayDrop);
        }

        [Import(typeof (FleetPlanLayVM))]
        public FleetPlanLayVM ViewModel
        {
            get { return DataContext as FleetPlanLayVM; }
            set { DataContext = value; }
        }

        private void OnPlanLayDrop(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            var details = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if (details == null || draggedItem == null)
            {
                return;
            }

            if (e.Effects != DragDropEffects.None)
            {
                if (draggedItem is PlanAircraftDTO)
                {
                    var planAircraft = draggedItem as PlanAircraftDTO;
                    ViewModel.OpenEditDialog(planAircraft, PlanDetailCreateSource.PlanAircraft);
                }
                else if (draggedItem is AircraftDTO)
                {
                    var aircraft = draggedItem as AircraftDTO;
                    PlanAircraftDTO planAircraft =
                        ViewModel.ViewPlanAircrafts.SourceCollection.Cast<PlanAircraftDTO>()
                            .Where(p => p.Id == aircraft.AircraftId)
                            .FirstOrDefault(pa => pa.IsOwn);
                    ViewModel.OpenEditDialog(planAircraft, PlanDetailCreateSource.Aircraft);
                }
            }

            e.Handled = true;
        }

        private void OnPlanLayDragOver(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            if (draggedItem == null || PlanLay == null) return;

            var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;
            if (dropDetails == null) return;
            dropDetails.CurrentDraggedOverItem = PlanLay;
            dropDetails.CurrentDropPosition = DropIndicationDetails.ConverDropPositionToString(DropPosition.Inside);

            e.Handled = true;
        }

        private void OnPlanAircraftDragInitialize(object sender, DragInitializeEventArgs e)
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

        private void OnAircraftDragInitialize(object sender, DragInitializeEventArgs e)
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
                ContentTemplate = gridView.Resources["OperationDraggedItemTemplate"] as DataTemplate
            };
            e.DragVisualOffset = e.RelativeStartPoint;
            e.AllowedEffects = DragDropEffects.All;
        }
    }
}
using System.ComponentModel.Composition;

namespace UniCloud.Presentation.FleetPlan.PerformFleetPlan
{
    [Export]
    public partial class PlanDeliverEditDialog
    {
        public PlanDeliverEditDialog()
        {
            InitializeComponent();
        }

        [Import(typeof(FleetPlanDeliverVM))]
        public FleetPlanDeliverVM ViewModel
        {
            get { return DataContext as FleetPlanDeliverVM; }
            set { DataContext = value; }
        }
    }
}


using System.ComponentModel.Composition;

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export(typeof(PlanDetailEditDialog))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PlanDetailEditDialog
    {
        public PlanDetailEditDialog()
        {
            InitializeComponent();
        }

        [Import]
        public FleetPlanLayVM ViewModel
        {
            get { return DataContext as FleetPlanLayVM; }
            set { DataContext = value; }
        }
    }
}

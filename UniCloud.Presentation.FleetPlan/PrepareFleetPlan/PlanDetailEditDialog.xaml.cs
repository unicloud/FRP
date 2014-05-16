
using System.ComponentModel.Composition;

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class PlanDetailEditDialog
    {
        public PlanDetailEditDialog()
        {
            InitializeComponent();
        }

        [Import(typeof(FleetPlanLayVM))]
        public FleetPlanLayVM ViewModel
        {
            get { return DataContext as FleetPlanLayVM; }
            set { DataContext = value; }
        }
    }
}

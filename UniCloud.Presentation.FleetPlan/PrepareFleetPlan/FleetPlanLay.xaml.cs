using System.ComponentModel.Composition;

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class FleetPlanLay
    {
        public FleetPlanLay()
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

using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class FleetPlanLay : UserControl
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

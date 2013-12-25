using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class FleetPlanPrepare : UserControl
    {
        public FleetPlanPrepare()
        {
            InitializeComponent();
        }
        [Import(typeof(FleetPlanPrepareVM))]
        public FleetPlanPrepareVM ViewModel
        {
            get { return DataContext as FleetPlanPrepareVM; }
            set { DataContext = value; }
        }
    }
}

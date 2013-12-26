using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.FleetPlan.PerformFleetPlan
{
    [Export]
    public partial class FleetPlanDeliver : UserControl
    {
        public FleetPlanDeliver()
        {
            InitializeComponent();
        }

        [Import(typeof (FleetPlanDeliverVM))]
        public FleetPlanDeliverVM ViewModel
        {
            get { return DataContext as FleetPlanDeliverVM; }
            set { DataContext = value; }
        }
    }
}

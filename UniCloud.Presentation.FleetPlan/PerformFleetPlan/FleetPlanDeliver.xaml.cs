#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.PerformFleetPlan
{
    [Export]
    public partial class FleetPlanDeliver
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
#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class FleetPlanPrepare
    {
        public FleetPlanPrepare()
        {
            InitializeComponent();
        }

        [Import(typeof (FleetPlanPrepareVM))]
        public FleetPlanPrepareVM ViewModel
        {
            get { return DataContext as FleetPlanPrepareVM; }
            set { DataContext = value; }
        }
    }
}
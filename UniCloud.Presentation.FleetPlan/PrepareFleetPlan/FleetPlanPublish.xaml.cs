#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class FleetPlanPublish
    {
        public FleetPlanPublish()
        {
            InitializeComponent();
        }

        [Import(typeof (FleetPlanPublishVM))]
        public FleetPlanPublishVM ViewModel
        {
            get { return DataContext as FleetPlanPublishVM; }
            set { DataContext = value; }
        }
    }
}
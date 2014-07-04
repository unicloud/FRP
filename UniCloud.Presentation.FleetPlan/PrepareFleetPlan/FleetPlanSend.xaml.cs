#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class FleetPlanSend
    {
        public FleetPlanSend()
        {
            InitializeComponent();
        }

        [Import(typeof (FleetPlanSendVM))]
        public FleetPlanSendVM ViewModel
        {
            get { return DataContext as FleetPlanSendVM; }
            set { DataContext = value; }
        }
    }
}
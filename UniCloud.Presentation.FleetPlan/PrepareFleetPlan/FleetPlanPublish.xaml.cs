using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class FleetPlanPublish : UserControl
    {
        public FleetPlanPublish()
        {
            InitializeComponent();
        }
        [Import(typeof(FleetPlanPublishVM))]
        public FleetPlanPublishVM ViewModel
        {
            get { return DataContext as FleetPlanPublishVM; }
            set { DataContext = value; }
        }
    }
}

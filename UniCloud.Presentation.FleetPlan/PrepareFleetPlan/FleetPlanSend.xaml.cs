using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class FleetPlanSend : UserControl
    {
        public FleetPlanSend()
        {
            InitializeComponent();
        }
        [Import(typeof(FleetPlanSendVM))]
        public FleetPlanSendVM ViewModel
        {
            get { return DataContext as FleetPlanSendVM; }
            set { DataContext = value; }
        }
    }
}

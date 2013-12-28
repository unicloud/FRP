using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof(CountRegisteredFleet))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class CountRegisteredFleet : UserControl
    {
        public CountRegisteredFleet()
        {
            InitializeComponent();
        }
        [Import]
        public CountRegisteredFleetVm ViewModel
        {
            get { return DataContext as CountRegisteredFleetVm; }
            set { DataContext = value; }
        }
    }
}

#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof(FleetAge))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class FleetAge : UserControl
    {
        public FleetAge()
        {
            InitializeComponent();
        }
        [Import]
        public FleetAgeVm ViewModel
        {
            get { return DataContext as FleetAgeVm; }
            set { DataContext = value; }
        }

        private void AircraftTypeToggleButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.ToggleButtonChecked(sender,e);
        }

        private void AircraftTypeToggleButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.ToggleButtonUnchecked(sender, e);
        }

    }
}

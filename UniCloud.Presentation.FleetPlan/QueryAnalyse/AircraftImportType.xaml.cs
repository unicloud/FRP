#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof(AircraftImportType))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AircraftImportType : UserControl
    {
        public AircraftImportType()
        {
            InitializeComponent();
        }
        [Import]
        public AircraftImportTypeVm ViewModel
        {
            get { return DataContext as AircraftImportTypeVm; }
            set { DataContext = value; }
        }

        private void ImportTypeToggleButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.ToggleButtonUnchecked(sender, e);
        }

        private void ImportTypeToggleButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.ToggleButtonChecked(sender, e);
        }
    }
}

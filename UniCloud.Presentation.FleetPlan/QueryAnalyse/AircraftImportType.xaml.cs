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
    }
}

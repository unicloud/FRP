#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export]
    public partial class AircraftImportType
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
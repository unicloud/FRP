#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftData
{
     [Export]
    public partial class ManagerAircraftLicense : UserControl
    {
        public ManagerAircraftLicense()
        {
            InitializeComponent();
        }

        [Import(typeof(ManagerAircraftLicenseVm))]
        public ManagerAircraftLicenseVm ViewModel
        {
            get { return DataContext as ManagerAircraftLicenseVm; }
            set { DataContext = value; }
        }
    }
}

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftData
{
    [Export]
    public partial class ManagerLicenseType
    {
        public ManagerLicenseType()
        {
            InitializeComponent();
        }

        [Import(typeof (ManagerLicenseTypeVm))]
        public ManagerLicenseTypeVm ViewModel
        {
            get { return DataContext as ManagerLicenseTypeVm; }
            set { DataContext = value; }
        }
    }
}
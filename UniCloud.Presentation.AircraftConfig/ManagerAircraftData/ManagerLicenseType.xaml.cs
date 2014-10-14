#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftData
{
    [Export]
    public partial class ManagerLicenseType : UserControl
    {
        public ManagerLicenseType()
        {
            InitializeComponent();
        }

        [Import(typeof(ManagerLicenseTypeVm))]
        public ManagerLicenseTypeVm ViewModel
        {
            get { return DataContext as ManagerLicenseTypeVm; }
            set { DataContext = value; }
        }
    }
}

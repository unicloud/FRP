#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftData
{
     [Export]
    public partial class ManagerAircraftLicense 
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

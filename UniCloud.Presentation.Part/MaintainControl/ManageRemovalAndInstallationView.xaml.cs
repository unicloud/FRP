using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.MaintainControl
{
    [Export(typeof (ManageRemovalAndInstallationView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageRemovalAndInstallationView : UserControl
    {
        public ManageRemovalAndInstallationView()
        {
            InitializeComponent();
        }

        [Import]
        public ManageRemovalAndInstallationVm ViewModel
        {
            get { return DataContext as ManageRemovalAndInstallationVm; }
            set { DataContext = value; }
        }
    }
}
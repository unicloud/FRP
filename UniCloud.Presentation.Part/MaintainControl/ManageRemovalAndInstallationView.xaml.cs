#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.MaintainControl
{
    [Export]
    public partial class ManageRemovalAndInstallationView
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
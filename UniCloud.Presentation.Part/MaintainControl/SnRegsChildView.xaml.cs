using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Part.MaintainControl
{
    [Export(typeof (SnRegsChildView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SnRegsChildView
    {
        public SnRegsChildView()
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
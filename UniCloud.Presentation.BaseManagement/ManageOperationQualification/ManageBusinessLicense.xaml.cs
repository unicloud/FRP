#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManageOperationQualification
{
    [Export]
    public partial class ManageBusinessLicense
    {
        public ManageBusinessLicense()
        {
            InitializeComponent();
        }

        [Import]
        public ManageBusinessLicenseVm ViewModel
        {
            get { return DataContext as ManageBusinessLicenseVm; }
            set { DataContext = value; }
        }
    }
}
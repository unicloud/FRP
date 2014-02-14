#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManageOperationQualification
{
    [Export(typeof(ManageOperationLicense))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageOperationLicense 
    {
        public ManageOperationLicense()
        {
            InitializeComponent();
        }
        [Import]
        public ManageOperationLicenseVm ViewModel
        {
            get { return DataContext as ManageOperationLicenseVm; }
            set { DataContext = value; }
        }
    }
}

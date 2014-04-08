#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof(ManageOrganizationInRole))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageOrganizationInRole 
    {
        public ManageOrganizationInRole()
        {
            InitializeComponent();
        }
        [Import]
        public ManageOrganizationInRoleVm ViewModel
        {
            get { return DataContext as ManageOrganizationInRoleVm; }
            set { DataContext = value; }
        }
    }
}

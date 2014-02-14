#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof(ManageUserInRole))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageUserInRole : UserControl
    {
        public ManageUserInRole()
        {
            InitializeComponent();
        }
        [Import]
        public ManageUserInRoleVm ViewModel
        {
            get { return DataContext as ManageUserInRoleVm; }
            set { DataContext = value; }
        }
    }
}

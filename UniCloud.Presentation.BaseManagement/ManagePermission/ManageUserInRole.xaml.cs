#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export]
    public partial class ManageUserInRole
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
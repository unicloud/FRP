#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export]
    public partial class ManageFunctionsInRole
    {
        public ManageFunctionsInRole()
        {
            InitializeComponent();
        }

        [Import]
        public ManageFunctionsInRoleVm ViewModel
        {
            get { return DataContext as ManageFunctionsInRoleVm; }
            set { DataContext = value; }
        }
    }
}
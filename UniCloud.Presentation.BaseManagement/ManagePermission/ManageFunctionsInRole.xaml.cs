#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof(ManageFunctionsInRole))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageFunctionsInRole : UserControl
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

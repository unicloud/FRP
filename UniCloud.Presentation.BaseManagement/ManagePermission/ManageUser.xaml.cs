#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof(ManageUser))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageUser : UserControl
    {
        public ManageUser()
        {
            InitializeComponent();
        }
        [Import]
        public ManageUserVm ViewModel
        {
            get { return DataContext as ManageUserVm; }
            set { DataContext = value; }
        }
    }
}

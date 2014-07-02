#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export]
    public partial class ManageUser
    {
        public ManageUser()
        {
            InitializeComponent();
        }

        [Import]
        public ManageUserVM ViewModel
        {
            get { return DataContext as ManageUserVM; }
            set { DataContext = value; }
        }
    }
}
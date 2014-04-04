#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;
using UniCloud.Presentation.Service.BaseManagement;

#endregion

namespace UniCloud.Presentation.Shell.Login
{
    [Export(typeof(LoginView))]
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            //ViewModel = new LoginVm(new BaseManagementService());
        }

        [Import(typeof(LoginVm))]
        public LoginVm ViewModel
        {
            get { return DataContext as LoginVm; }
            set { DataContext = value; }
        }
    }
}

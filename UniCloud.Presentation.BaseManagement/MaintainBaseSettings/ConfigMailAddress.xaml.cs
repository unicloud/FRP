#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.MaintainBaseSettings
{
    [Export]
    public partial class ConfigMailAddress
    {
        public ConfigMailAddress()
        {
            InitializeComponent();
        }

        [Import]
        public ConfigMailAddressVm ViewModel
        {
            get { return DataContext as ConfigMailAddressVm; }
            set { DataContext = value; }
        }
    }
}
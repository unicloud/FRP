#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.BaseManagement.MaintainBaseSettings
{
    [Export(typeof(ConfigMailAddress))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ConfigMailAddress : UserControl
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

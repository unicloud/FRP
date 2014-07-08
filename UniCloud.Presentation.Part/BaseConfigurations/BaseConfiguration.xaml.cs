#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.BaseConfigurations
{
    [Export]
    public partial class BaseConfiguration
    {
        public BaseConfiguration()
        {
            InitializeComponent();
        }

        [Import(typeof (BaseConfigurationVm))]
        public BaseConfigurationVm ViewModel
        {
            get { return DataContext as BaseConfigurationVm; }
            set { DataContext = value; }
        }
    }
}
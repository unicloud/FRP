#region NameSpace

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Part.BaseConfigurations
{
    [Export]
    public partial class BaseConfiguration : UserControl
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
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export]
    public partial class ConfigCompareView : UserControl
    {
        public ConfigCompareView()
        {
            InitializeComponent();
        }

        [Import(typeof (ConfigCompareVm))]
        public ConfigCompareVm ViewModel
        {
            get { return DataContext as ConfigCompareVm; }
            set { DataContext = value; }
        }
    }
}
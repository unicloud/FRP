#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export]
    public partial class ConfigCompareView
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
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export]
    public partial class SpecialConfigView : UserControl
    {
        public SpecialConfigView()
        {
            InitializeComponent();
        }

        [Import(typeof (SpecialConfigVm))]
        public SpecialConfigVm ViewModel
        {
            get { return DataContext as SpecialConfigVm; }
            set { DataContext = value; }
        }
    }
}
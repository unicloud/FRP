using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export]
    public partial class BasicConfigGroupView : UserControl
    {
        public BasicConfigGroupView()
        {
            InitializeComponent();
        }

        [Import(typeof (BasicConfigGroupVm))]
        public BasicConfigGroupVm ViewModel
        {
            get { return DataContext as BasicConfigGroupVm; }
            set { DataContext = value; }
        }
    }
}
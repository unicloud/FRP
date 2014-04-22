using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export]
    public partial class InSeatAssemblyQueryView : UserControl
    {
        public InSeatAssemblyQueryView()
        {
            InitializeComponent();
        }

        [Import(typeof (InSeatAssemblyQueryVm))]
        public InSeatAssemblyQueryVm ViewModel
        {
            get { return DataContext as InSeatAssemblyQueryVm; }
            set { DataContext = value; }
        }
    }
}
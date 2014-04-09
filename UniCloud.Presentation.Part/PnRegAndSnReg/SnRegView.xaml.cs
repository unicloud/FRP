using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.PnRegAndSnReg
{
    [Export(typeof (SnRegView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SnRegView : UserControl
    {
        public SnRegView()
        {
            InitializeComponent();
        }

        [Import]
        public SnRegVm ViewModel
        {
            get { return DataContext as SnRegVm; }
            set { DataContext = value; }
        }
    }
}
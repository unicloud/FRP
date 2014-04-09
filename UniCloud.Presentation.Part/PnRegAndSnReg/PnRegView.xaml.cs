using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.PnRegAndSnReg
{
    [Export(typeof (PnRegView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PnRegView : UserControl
    {
        public PnRegView()
        {
            InitializeComponent();
        }

        [Import]
        public PnRegVm ViewModel
        {
            get { return DataContext as PnRegVm; }
            set { DataContext = value; }
        }
    }
}
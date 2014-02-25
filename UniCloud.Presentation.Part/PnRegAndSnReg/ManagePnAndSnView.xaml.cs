using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.PnRegAndSnReg
{
    [Export]
    public partial class ManagePnAndSnView : UserControl
    {
        public ManagePnAndSnView()
        {
            InitializeComponent();
        }

        [Import(typeof (ManagePnAndSnVm))]
        public ManagePnAndSnVm ViewModel
        {
            get { return DataContext as ManagePnAndSnVm; }
            set { DataContext = value; }
        }
    }
}
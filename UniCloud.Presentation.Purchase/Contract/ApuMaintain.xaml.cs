using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export]
    public partial class ApuMaintain : UserControl
    {
        public ApuMaintain()
        {
            InitializeComponent();
        }

        [Import(typeof(ApuMaintainVm))]
        public ApuMaintainVm ViewModel
        {
            get { return DataContext as ApuMaintainVm; }
            set { DataContext = value; }
        }
    }
}

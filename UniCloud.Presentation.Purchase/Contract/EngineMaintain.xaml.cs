using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export]
    public partial class EngineMaintain : UserControl
    {
        public EngineMaintain()
        {
            InitializeComponent();
        }

        [Import(typeof(EngineMaintainVm))]
        public EngineMaintainVm ViewModel
        {
            get { return DataContext as EngineMaintainVm; }
            set { DataContext = value; }
        }
    }
}

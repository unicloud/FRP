using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export]
    public partial class UndercartMaintain : UserControl
    {
        public UndercartMaintain()
        {
            InitializeComponent();
        }

        [Import(typeof(UndercartMaintainVm))]
        public UndercartMaintainVm ViewModel
        {
            get { return DataContext as UndercartMaintainVm; }
            set { DataContext = value; }
        }
    }
}

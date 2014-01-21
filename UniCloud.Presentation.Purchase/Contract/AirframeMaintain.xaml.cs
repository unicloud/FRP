#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export]
    public partial class AirframeMaintain : UserControl
    {
        public AirframeMaintain()
        {
            InitializeComponent();
        }

        [Import(typeof(AirframeMaintainVm))]
        public AirframeMaintainVm ViewModel
        {
            get { return DataContext as AirframeMaintainVm; }
            set { DataContext = value; }
        }
    }
}

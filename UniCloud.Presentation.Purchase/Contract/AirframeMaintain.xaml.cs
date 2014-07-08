#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export]
    public partial class AirframeMaintain
    {
        public AirframeMaintain()
        {
            InitializeComponent();
        }

        [Import(typeof (AirframeMaintainVm))]
        public AirframeMaintainVm ViewModel
        {
            get { return DataContext as AirframeMaintainVm; }
            set { DataContext = value; }
        }
    }
}
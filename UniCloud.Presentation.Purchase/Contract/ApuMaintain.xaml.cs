#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export]
    public partial class ApuMaintain
    {
        public ApuMaintain()
        {
            InitializeComponent();
        }

        [Import(typeof (ApuMaintainVm))]
        public ApuMaintainVm ViewModel
        {
            get { return DataContext as ApuMaintainVm; }
            set { DataContext = value; }
        }
    }
}
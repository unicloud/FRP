#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export]
    public partial class UndercartMaintain
    {
        public UndercartMaintain()
        {
            InitializeComponent();
        }

        [Import(typeof (UndercartMaintainVm))]
        public UndercartMaintainVm ViewModel
        {
            get { return DataContext as UndercartMaintainVm; }
            set { DataContext = value; }
        }
    }
}
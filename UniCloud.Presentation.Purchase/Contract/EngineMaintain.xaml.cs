#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export]
    public partial class EngineMaintain
    {
        public EngineMaintain()
        {
            InitializeComponent();
        }

        [Import(typeof (EngineMaintainVm))]
        public EngineMaintainVm ViewModel
        {
            get { return DataContext as EngineMaintainVm; }
            set { DataContext = value; }
        }
    }
}
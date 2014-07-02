#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export]
    public partial class RegularCheckMaintainCostManage
    {
        public RegularCheckMaintainCostManage()
        {
            InitializeComponent();
        }

        [Import]
        public RegularCheckMaintainCostManageVm ViewModel
        {
            get { return DataContext as RegularCheckMaintainCostManageVm; }
            set { DataContext = value; }
        }
    }
}
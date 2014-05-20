#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export(typeof(UndercartMaintainCostManage))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class UndercartMaintainCostManage 
    {
        public UndercartMaintainCostManage()
        {
            InitializeComponent();
        }

        [Import]
        public UndercartMaintainCostManageVm ViewModel
        {
            get { return DataContext as UndercartMaintainCostManageVm; }
            set { DataContext = value; }
        }
    }
}

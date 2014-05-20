#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export(typeof(FhaMaintainCostManage))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class FhaMaintainCostManage 
    {
        public FhaMaintainCostManage()
        {
            InitializeComponent();
        }

        [Import]
        public FhaMaintainCostManageVm ViewModel
        {
            get { return DataContext as FhaMaintainCostManageVm; }
            set { DataContext = value; }
        }
    }
}

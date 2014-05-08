#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageAnnualMaintainPlan
{
    [Export(typeof(ManageEngineMaintainPlan))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageEngineMaintainPlan 
    {
        public ManageEngineMaintainPlan()
        {
            InitializeComponent();
        }
        [Import]
        public ManageEngineMaintainPlanVm ViewModel
        {
            get { return DataContext as ManageEngineMaintainPlanVm; }
            set { DataContext = value; }
        }
    }
}

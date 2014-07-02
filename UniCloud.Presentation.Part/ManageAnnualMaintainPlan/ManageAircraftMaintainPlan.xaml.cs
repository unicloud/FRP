#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageAnnualMaintainPlan
{
    [Export]
    public partial class ManageAircraftMaintainPlan
    {
        public ManageAircraftMaintainPlan()
        {
            InitializeComponent();
        }

        [Import]
        public ManageAircraftMaintainPlanVm ViewModel
        {
            get { return DataContext as ManageAircraftMaintainPlanVm; }
            set { DataContext = value; }
        }
    }
}
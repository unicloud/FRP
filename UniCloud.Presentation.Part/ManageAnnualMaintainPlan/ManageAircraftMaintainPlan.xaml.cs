#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Part.ManageAnnualMaintainPlan
{
    [Export(typeof(ManageAircraftMaintainPlan))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageAircraftMaintainPlan : UserControl
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

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export]
    public partial class FleetAge
    {
        public FleetAge()
        {
            InitializeComponent();
        }

        [Import]
        public FleetAgeVm ViewModel
        {
            get { return DataContext as FleetAgeVm; }
            set { DataContext = value; }
        }
    }
}
#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export]
    public partial class FleetTrend
    {
        public FleetTrend()
        {
            InitializeComponent();
        }

        [Import]
        public FleetTrendVm ViewModel
        {
            get { return DataContext as FleetTrendVm; }
            set { DataContext = value; }
        }
    }
}
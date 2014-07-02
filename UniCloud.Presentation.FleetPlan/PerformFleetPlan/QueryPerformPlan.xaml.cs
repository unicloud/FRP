#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.PerformFleetPlan
{
    [Export]
    public partial class QueryPerformPlan
    {
        public QueryPerformPlan()
        {
            InitializeComponent();
        }

        [Import]
        public QueryPerformPlanVM ViewModel
        {
            get { return DataContext as QueryPerformPlanVM; }
            set { DataContext = value; }
        }
    }
}
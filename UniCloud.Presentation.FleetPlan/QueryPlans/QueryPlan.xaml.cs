#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryPlans
{
    [Export]
    public partial class QueryPlan
    {
        public QueryPlan()
        {
            InitializeComponent();
        }

        [Import]
        public QueryPlanVM ViewModel
        {
            get { return DataContext as QueryPlanVM; }
            set { DataContext = value; }
        }
    }
}
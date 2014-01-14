using System.ComponentModel.Composition;

namespace UniCloud.Presentation.FleetPlan.QueryPlans
{
    [Export(typeof(QueryPlan))]
    [PartCreationPolicy(CreationPolicy.Shared)]
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

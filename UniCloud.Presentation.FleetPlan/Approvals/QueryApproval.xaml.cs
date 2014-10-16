#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.Approvals
{
    [Export(typeof(QueryApproval))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class QueryApproval
    {
        public QueryApproval()
        {
            InitializeComponent();
        }

        [Import]
        public QueryApprovalVM ViewModel
        {
            get { return DataContext as QueryApprovalVM; }
            set { DataContext = value; }
        }
    }
}
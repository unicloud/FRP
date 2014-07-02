#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{
    [Export]
    public partial class QueryRequest
    {
        public QueryRequest()
        {
            InitializeComponent();
        }

        [Import]
        public QueryRequestVM ViewModel
        {
            get { return DataContext as QueryRequestVM; }
            set { DataContext = value; }
        }
    }
}
#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{
    [Export(typeof (Request))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class Request
    {
        public Request()
        {
            InitializeComponent();
        }

        [Import]
        public RequestVM ViewModel
        {
            get { return DataContext as RequestVM; }
            set { DataContext = value; }
        }
    }
}
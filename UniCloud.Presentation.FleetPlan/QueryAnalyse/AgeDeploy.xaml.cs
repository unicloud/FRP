#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof(AgeDeploy))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AgeDeploy 
    {
        public AgeDeploy()
        {
            InitializeComponent();
        }
        [Import]
        public AgeDeployVm ViewModel
        {
            get { return DataContext as AgeDeployVm; }
            set { DataContext = value; }
        }
    }
}

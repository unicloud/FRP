#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.PerformFleetPlan
{
    [Export(typeof (OperationChild))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class OperationChild
    {
        public OperationChild()
        {
            InitializeComponent();
        }
        [Import(typeof(OperationChildVM))]
        public OperationChildVM ViewModel
        {
            get { return DataContext as OperationChildVM; }
            set { DataContext = value; }
        }
    }
}
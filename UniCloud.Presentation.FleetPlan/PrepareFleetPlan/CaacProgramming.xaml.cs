#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class CaacProgramming
    {
        public CaacProgramming()
        {
            InitializeComponent();
        }

        [Import(typeof (CaacProgrammingVM))]
        public CaacProgrammingVM ViewModel
        {
            get { return DataContext as CaacProgrammingVM; }
            set { DataContext = value; }
        }
    }
}
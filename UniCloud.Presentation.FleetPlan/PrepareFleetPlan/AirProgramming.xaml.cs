#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class AirProgramming
    {
        public AirProgramming()
        {
            InitializeComponent();
        }

        [Import(typeof (AirProgrammingVM))]
        public AirProgrammingVM ViewModel
        {
            get { return DataContext as AirProgrammingVM; }
            set { DataContext = value; }
        }
    }
}
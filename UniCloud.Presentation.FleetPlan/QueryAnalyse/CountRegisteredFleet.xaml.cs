#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export]
    public partial class CountRegisteredFleet
    {
        public CountRegisteredFleet()
        {
            InitializeComponent();
        }

        [Import]
        public CountRegisteredFleetVm ViewModel
        {
            get { return DataContext as CountRegisteredFleetVm; }
            set { DataContext = value; }
        }
    }
}
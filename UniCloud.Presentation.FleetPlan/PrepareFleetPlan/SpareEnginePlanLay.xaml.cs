#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class SpareEnginePlanLay
    {
        public SpareEnginePlanLay()
        {
            InitializeComponent();
        }

        [Import(typeof (SpareEnginePlanLayVM))]
        public SpareEnginePlanLayVM ViewModel
        {
            get { return DataContext as SpareEnginePlanLayVM; }
            set { DataContext = value; }
        }
    }
}
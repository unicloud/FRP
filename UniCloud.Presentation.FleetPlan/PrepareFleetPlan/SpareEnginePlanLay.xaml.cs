using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class SpareEnginePlanLay : UserControl
    {
        public SpareEnginePlanLay()
        {
            InitializeComponent();
        }
        [Import(typeof(SpareEnginePlanLayVM))]
        public SpareEnginePlanLayVM ViewModel
        {
            get { return DataContext as SpareEnginePlanLayVM; }
            set { DataContext = value; }
        }
    }
}

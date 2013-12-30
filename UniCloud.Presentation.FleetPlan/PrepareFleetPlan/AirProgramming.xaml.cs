using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class AirProgramming : UserControl
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

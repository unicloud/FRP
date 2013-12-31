using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export]
    public partial class CaacProgramming : UserControl
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

using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public partial class MatchingPlanAircraftManager : UserControl
    {
        public MatchingPlanAircraftManager()
        {
            InitializeComponent();
        }

        [Import(typeof (MatchingPlanAircraftManagerVM))]
        public MatchingPlanAircraftManagerVM ViewModel
        {
            get { return DataContext as MatchingPlanAircraftManagerVM; }
            set { DataContext = value; }
        }
    }
}
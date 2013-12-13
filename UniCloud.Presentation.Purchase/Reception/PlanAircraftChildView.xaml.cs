using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export(typeof(PlanAircraftChildView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PlanAircraftChildView
    {
        public PlanAircraftChildView()
        {
            InitializeComponent();
        }

        [Import]
        public MatchingPlanAircraftManagerVM ViewModel
        {
            get { return DataContext as MatchingPlanAircraftManagerVM; }
            set { DataContext = value; }
        }
    }
}

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
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
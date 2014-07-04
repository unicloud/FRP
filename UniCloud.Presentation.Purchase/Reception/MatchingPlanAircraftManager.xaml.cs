#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public partial class MatchingPlanAircraftManager
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
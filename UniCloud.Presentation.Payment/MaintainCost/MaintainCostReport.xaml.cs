#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export]
    public partial class MaintainCostReport
    {
        public MaintainCostReport()
        {
            InitializeComponent();
        }

        [Import]
        public MaintainCostReportVm ViewModel
        {
            get { return DataContext as MaintainCostReportVm; }
            set { DataContext = value; }
        }
    }
}
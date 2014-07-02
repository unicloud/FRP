#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.QueryAnalyse
{
    [Export]
    public partial class AnalyseMaintenanceCosts
    {
        public AnalyseMaintenanceCosts()
        {
            InitializeComponent();
        }

        [Import]
        public AnalyseMaintenanceCostsVM ViewModel
        {
            get { return DataContext as AnalyseMaintenanceCostsVM; }
            set { DataContext = value; }
        }
    }
}
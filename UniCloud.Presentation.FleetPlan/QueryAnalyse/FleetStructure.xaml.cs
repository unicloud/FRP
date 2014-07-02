#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export]
    public partial class FleetStructure
    {
        public FleetStructure()
        {
            InitializeComponent();
        }

        [Import]
        public FleetStructureVm ViewModel
        {
            get { return DataContext as FleetStructureVm; }
            set { DataContext = value; }
        }
    }
}
#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export]
    public partial class EngineImportType
    {
        public EngineImportType()
        {
            InitializeComponent();
        }

        [Import]
        public EngineImportTypeVm ViewModel
        {
            get { return DataContext as EngineImportTypeVm; }
            set { DataContext = value; }
        }
    }
}
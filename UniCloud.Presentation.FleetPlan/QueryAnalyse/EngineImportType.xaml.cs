#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof(EngineImportType))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class EngineImportType : UserControl
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

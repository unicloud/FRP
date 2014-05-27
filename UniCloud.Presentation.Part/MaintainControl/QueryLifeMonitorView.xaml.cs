using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace UniCloud.Presentation.Part.MaintainControl
{
    [Export(typeof (QueryLifeMonitorView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class QueryLifeMonitorView : UserControl
    {
        public QueryLifeMonitorView()
        {
            InitializeComponent();
        }

        [Import]
        public QueryLifeMonitorVm ViewModel
        {
            get { return DataContext as QueryLifeMonitorVm; }
            set { DataContext = value; }
        }
    }
}
#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.MaintainControl
{
    [Export]
    public partial class QueryLifeMonitorView
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
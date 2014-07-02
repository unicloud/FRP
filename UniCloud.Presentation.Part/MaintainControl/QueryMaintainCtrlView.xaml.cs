#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.MaintainControl
{
    [Export]
    public partial class QueryMaintainCtrlView
    {
        public QueryMaintainCtrlView()
        {
            InitializeComponent();
        }

        [Import]
        public QueryMaintainCtrlVm ViewModel
        {
            get { return DataContext as QueryMaintainCtrlVm; }
            set { DataContext = value; }
        }
    }
}
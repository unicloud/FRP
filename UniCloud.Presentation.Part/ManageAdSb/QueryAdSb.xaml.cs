#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageAdSb
{
    [Export]
    public partial class QueryAdSb
    {
        public QueryAdSb()
        {
            InitializeComponent();
        }

        [Import]
        public QueryAdSbVm ViewModel
        {
            get { return DataContext as QueryAdSbVm; }
            set { DataContext = value; }
        }
    }
}
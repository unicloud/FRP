#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Part.ManageAdSb
{
    [Export(typeof(QueryAdSb))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class QueryAdSb : UserControl
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

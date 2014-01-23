#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.CommonService.SearchDocument
{
    [Export]
    public partial class SearchDocumentMain : UserControl
    {
        public SearchDocumentMain()
        {
            InitializeComponent();
        }
        [Import(typeof(SearchDocumentMainVm))]
        public SearchDocumentMainVm ViewModel
        {
            get { return DataContext as SearchDocumentMainVm; }
            set { DataContext = value; }
        }
    }
}

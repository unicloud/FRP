#region 命名空间

using System.ComponentModel.Composition;
using System.Windows.Controls;

#endregion

namespace UniCloud.Presentation.CommonService.SearchDocument
{
    [Export]
    public partial class SearchDocument : UserControl
    {
        public SearchDocument()
        {
            InitializeComponent();
        }

        [Import(typeof(SearchDocumentVm))]
        public SearchDocumentVm ViewModel
        {
            get { return DataContext as SearchDocumentVm; }
            set { DataContext = value; }
        }

       
    }
}

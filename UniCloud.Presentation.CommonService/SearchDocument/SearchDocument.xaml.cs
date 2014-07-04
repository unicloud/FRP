#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.CommonService.SearchDocument
{
    [Export]
    public partial class SearchDocument
    {
        public SearchDocument()
        {
            InitializeComponent();
        }

        [Import(typeof (SearchDocumentVm))]
        public SearchDocumentVm ViewModel
        {
            get { return DataContext as SearchDocumentVm; }
            set { DataContext = value; }
        }
    }
}
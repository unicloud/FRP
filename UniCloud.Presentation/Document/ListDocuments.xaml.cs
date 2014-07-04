#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export(typeof(ListDocuments))]
    public partial class ListDocuments 
    {
        public ListDocuments()
        {
            InitializeComponent();
        }

        [Import(typeof(ListDocumentsVm))]
        public ListDocumentsVm ViewModel
        {
            get { return DataContext as ListDocumentsVm; }
            set { DataContext = value; }
        }
    }
}

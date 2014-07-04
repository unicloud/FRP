#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.CommonService.DocumentTypeManager
{
    [Export]
    public partial class ManagerDocumentType
    {
        public ManagerDocumentType()
        {
            InitializeComponent();
        }

        [Import(typeof (ManagerDocumentTypeVm))]
        public ManagerDocumentTypeVm ViewModel
        {
            get { return DataContext as ManagerDocumentTypeVm; }
            set { DataContext = value; }
        }
    }
}
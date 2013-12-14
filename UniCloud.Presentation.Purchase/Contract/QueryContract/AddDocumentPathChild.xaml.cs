#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof(AddDocumentPathChild))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class AddDocumentPathChild
    {
        public AddDocumentPathChild()
        {
            InitializeComponent();
        }

        [Import]
        public QueryContractVM ViewModel
        {
            get { return DataContext as QueryContractVM; }
            set { DataContext = value; }
        }
    }
}
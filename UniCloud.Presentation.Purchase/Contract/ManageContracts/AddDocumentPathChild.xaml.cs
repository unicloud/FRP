#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract.ManageContracts
{
    [Export]
    public partial class AddDocumentPathChild
    {
        public AddDocumentPathChild()
        {
            InitializeComponent();
        }

        [Import]
        public ManageContractVm ViewModel
        {
            get { return DataContext as ManageContractVm; }
            set { DataContext = value; }
        }
    }
}
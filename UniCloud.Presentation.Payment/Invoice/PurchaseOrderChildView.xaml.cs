#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export]
    public partial class PurchaseOrderChildView
    {
        public PurchaseOrderChildView()
        {
            InitializeComponent();
        }

        [Import]
        public PurchaseCreditNoteManagerVm ViewModel
        {
            get { return DataContext as PurchaseCreditNoteManagerVm; }
            set { DataContext = value; }
        }
    }
}
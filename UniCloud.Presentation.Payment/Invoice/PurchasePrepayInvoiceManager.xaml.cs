#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(PurchasePrepayInvoiceManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PurchasePrepayInvoiceManager
    {
        public PurchasePrepayInvoiceManager()
        {
            InitializeComponent();
        }
        [Import]
        public PurchasePrepayInvoiceManagerVm ViewModel
        {
            get { return DataContext as PurchasePrepayInvoiceManagerVm; }
            set { DataContext = value; }
        }
    }
}

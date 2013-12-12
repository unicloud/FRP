using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(PurchaseInvoiceManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PurchaseInvoiceManager
    {
        public PurchaseInvoiceManager()
        {
            InitializeComponent();
        }
        [Import]
        public PurchaseInvoiceManagerVM ViewModel
        {
            get { return DataContext as PurchaseInvoiceManagerVM; }
            set { DataContext = value; }
        }
    }
}

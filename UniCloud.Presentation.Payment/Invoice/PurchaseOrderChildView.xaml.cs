using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(PurchaseOrderChildView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PurchaseOrderChildView
    {
        public PurchaseOrderChildView()
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


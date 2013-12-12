using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(LeaseInvoiceManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class LeaseInvoiceManager
    {
        public LeaseInvoiceManager()
        {
            InitializeComponent();
        }
        [Import]
        public LeaseInvoiceManagerVM ViewModel
        {
            get { return DataContext as LeaseInvoiceManagerVM; }
            set { DataContext = value; }
        }
    }
}

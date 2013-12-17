using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(LeaseOrderChildView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class LeaseOrderChildView
    {
        public LeaseOrderChildView()
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


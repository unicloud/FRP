using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(PrepaymentOrderChildView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PrepaymentOrderChildView
    {
        public PrepaymentOrderChildView()
        {
            InitializeComponent();
        }

        [Import]
        public PrePayInvoiceManagerVM ViewModel
        {
            get { return DataContext as PrePayInvoiceManagerVM; }
            set { DataContext = value; }
        }
    }
}


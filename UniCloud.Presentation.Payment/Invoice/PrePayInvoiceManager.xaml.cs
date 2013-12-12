using System.ComponentModel.Composition;


namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(PrePayInvoiceManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PrePayInvoiceManager
    {
        public PrePayInvoiceManager()
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

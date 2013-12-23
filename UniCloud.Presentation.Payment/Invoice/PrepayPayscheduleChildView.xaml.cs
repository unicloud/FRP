using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(PrepayPayscheduleChildView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PrepayPayscheduleChildView
    {
        public PrepayPayscheduleChildView()
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


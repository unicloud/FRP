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
        public CreditNoteManagerVM ViewModel
        {
            get { return DataContext as CreditNoteManagerVM; }
            set { DataContext = value; }
        }
    }
}


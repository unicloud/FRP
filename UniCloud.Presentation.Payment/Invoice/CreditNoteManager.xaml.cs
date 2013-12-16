using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(CreditNoteManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class CreditNoteManager
    {
        public CreditNoteManager()
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

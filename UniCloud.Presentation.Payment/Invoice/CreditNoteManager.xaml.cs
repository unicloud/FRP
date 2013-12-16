#region 命名空间

using System.ComponentModel.Composition;

#endregion

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

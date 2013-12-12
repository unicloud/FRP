using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(CreditMemoManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class CreditMemoManager
    {
        public CreditMemoManager()
        {
            InitializeComponent();
        }
        [Import]
        public CreditMemoManagerVM ViewModel
        {
            get { return DataContext as CreditMemoManagerVM; }
            set { DataContext = value; }
        }
    }
}

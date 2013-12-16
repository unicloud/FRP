#region 命名空间

using System.ComponentModel.Composition;

#endregion

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

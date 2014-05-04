#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(PurchasePayscheduleChildView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PurchasePayscheduleChildView
    {
        public PurchasePayscheduleChildView()
        {
            InitializeComponent();
        }

        [Import]
        public PurchaseInvoiceManagerVM ViewModel
        {
            get { return DataContext as PurchaseInvoiceManagerVM; }
            set { DataContext = value; }
        }
    }
}


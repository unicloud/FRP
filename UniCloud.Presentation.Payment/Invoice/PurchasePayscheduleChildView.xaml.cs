#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export]
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
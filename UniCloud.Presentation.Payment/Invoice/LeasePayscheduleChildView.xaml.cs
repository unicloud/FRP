#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export]
    public partial class LeasePayscheduleChildView
    {
        public LeasePayscheduleChildView()
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
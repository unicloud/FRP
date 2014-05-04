#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(SundryInvoiceManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SundryInvoiceManager 
    {
        public SundryInvoiceManager()
        {
            InitializeComponent();
        }
        [Import]
        public SundryInvoiceManagerVm ViewModel
        {
            get { return DataContext as SundryInvoiceManagerVm; }
            set { DataContext = value; }
        }
    }
}

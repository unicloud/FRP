#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(SpecialRefitInvoiceManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SpecialRefitInvoiceManager 
    {
        public SpecialRefitInvoiceManager()
        {
            InitializeComponent();
        }
        [Import]
        public SpecialRefitInvoiceManagerVm ViewModel
        {
            get { return DataContext as SpecialRefitInvoiceManagerVm; }
            set { DataContext = value; }
        }
    }
}

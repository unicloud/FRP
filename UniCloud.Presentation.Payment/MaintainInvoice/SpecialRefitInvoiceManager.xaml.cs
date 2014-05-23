#region 命名空间

using System.ComponentModel.Composition;
using UniCloud.Presentation.Payment.Invoice;

#endregion

namespace UniCloud.Presentation.Payment.MaintainInvoice
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

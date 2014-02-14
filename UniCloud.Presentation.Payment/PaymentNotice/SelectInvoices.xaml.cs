#region 命名空间

using System.ComponentModel.Composition;
using UniCloud.Presentation.Service.Payment;

#endregion

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    [Export(typeof(SelectInvoices))]
    public partial class SelectInvoices
    {
        public SelectInvoices()
        {
            InitializeComponent();
            ViewModel = new SelectInvoicesVm(this,new PaymentService());
        }
        
        public SelectInvoicesVm ViewModel
        {
            get { return DataContext as SelectInvoicesVm; }
            set { DataContext = value; }
        }
    }
}

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    [Export(typeof(SelectInvoices))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SelectInvoices 
    {
        public SelectInvoices()
        {
            InitializeComponent();
        }
        [Import]
        public PaymentNoticeVm ViewModel
        {
            get { return DataContext as PaymentNoticeVm; }
            set { DataContext = value; }
        }
    }
}

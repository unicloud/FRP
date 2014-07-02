#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export]
    public partial class MaintainPrepayPayscheduleChildView
    {
        public MaintainPrepayPayscheduleChildView()
        {
            InitializeComponent();
        }

        [Import]
        public MaintainPrepayInvoiceManagerVm ViewModel
        {
            get { return DataContext as MaintainPrepayInvoiceManagerVm; }
            set { DataContext = value; }
        }
    }
}
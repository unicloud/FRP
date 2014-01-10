using UniCloud.Presentation.Service.Payment;

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    public partial class PaymentNoticeEdit
    {
        public PaymentNoticeEdit()
        {
            InitializeComponent();
            ViewModel = new PaymentNoticeEditVm(this,new PaymentService());
        }

        public PaymentNoticeEditVm ViewModel
        {
            get { return DataContext as PaymentNoticeEditVm; }
            set { DataContext = value; }
        }
    }
}

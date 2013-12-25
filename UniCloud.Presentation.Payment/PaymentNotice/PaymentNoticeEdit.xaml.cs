namespace UniCloud.Presentation.Payment.PaymentNotice
{
    public partial class PaymentNoticeEdit
    {
        public PaymentNoticeEdit()
        {
            InitializeComponent();
            ViewModel = new PaymentNoticeEditVm(this);
        }

        public PaymentNoticeEditVm ViewModel
        {
            get { return DataContext as PaymentNoticeEditVm; }
            set { DataContext = value; }
        }
    }
}

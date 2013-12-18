using System.ComponentModel.Composition;


namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export(typeof(StandardPaymentSchedule))]
    [PartCreationPolicy(CreationPolicy.Shared)]

    public partial class StandardPaymentSchedule
    {
        public StandardPaymentSchedule()
        {
            InitializeComponent();
        }
        [Import]
        public StandardPaymentScheduleVM ViewModel
        {
            get { return DataContext as StandardPaymentScheduleVM; }
            set { DataContext = value; }
        }
    }
}
